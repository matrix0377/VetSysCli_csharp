using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetSysCli.Application.DTOs;
using VetSysCli.Application.Interfaces;
using VetSysCli.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace VetSysCli.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _repo;
        private readonly string[] _allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".tif", ".webp" };
        private readonly string _photoFolder;

        public AnimalService(IAnimalRepository repo)
        {
            _repo = repo;
            _photoFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            Directory.CreateDirectory(_photoFolder);
        }

        public async Task<AnimalDto> CreateAsync(CreateAnimalDto dto)
        {
            var entity = new Animal
            {
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                VeterinarianId = dto.VeterinarianId,
                Type = dto.Type,
                Breed = dto.Breed,
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                PhysicalCharacteristics = dto.PhysicalCharacteristics,
                Notes = dto.Notes
            };
            var created = await _repo.AddAsync(entity);
            return Map(created);
        }

        public async Task<AnimalDto> GetByIdAsync(Guid id)
        {
            var a = await _repo.GetByIdAsync(id);
            return a == null ? null : Map(a);
        }

        public async Task<IEnumerable<AnimalDto>> SearchAsync(string query)
        {
            var list = await _repo.SearchAsync(query);
            return list.Select(Map);
        }

        public async Task UploadPhotoAsync(Guid id, IFormFile file)
        {
            if (file == null) throw new ArgumentException("Arquivo inválido");
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowed.Contains(ext)) throw new ArgumentException("Formato não permitido");

            var animal = await _repo.GetByIdAsync(id);
            if (animal == null) throw new KeyNotFoundException("Animal não encontrado");

            var fileName = $"{id}{ext}";
            var path = Path.Combine(_photoFolder, fileName);
            using var fs = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fs);

            animal.PhotoPath = $"/uploads/{fileName}";
            await _repo.UpdateAsync(animal);
        }

        private AnimalDto Map(Animal a)
        {
            return new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type,
                Breed = a.Breed,
                BirthDate = a.BirthDate,
                Age = CalculateAge(a.BirthDate),
                PhotoPath = a.PhotoPath
            };
        }

        private string CalculateAge(DateTime? birth)
        {
            if (!birth.HasValue) return "Desconhecida";
            var now = DateTime.UtcNow;
            var years = now.Year - birth.Value.Year;
            var months = now.Month - birth.Value.Month;
            var days = now.Day - birth.Value.Day;
            if (days < 0) { months--; days += DateTime.DaysInMonth(now.Year, now.Month - 1); }
            if (months < 0) { years--; months += 12; }
            return $"{years} anos, {months} meses";
        }
    }
}
