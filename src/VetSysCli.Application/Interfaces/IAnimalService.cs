using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetSysCli.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace VetSysCli.Application.Interfaces
{
    public interface IAnimalService
    {
        Task<IEnumerable<AnimalDto>> SearchAsync(string query);
        Task<AnimalDto> GetByIdAsync(Guid id);
        Task<AnimalDto> CreateAsync(CreateAnimalDto dto);
        Task UploadPhotoAsync(Guid id, IFormFile file);
    }
}
