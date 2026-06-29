using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetSysCli.Core.Entities;
using VetSysCli.Infrastructure.Data;

namespace VetSysCli.Infrastructure.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly VetSysDbContext _db;
        public AnimalRepository(VetSysDbContext db) => _db = db;

        public async Task<Animal> AddAsync(Animal animal)
        {
            _db.Animals.Add(animal);
            await _db.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> GetByIdAsync(Guid id) =>
            await _db.Animals.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Animal>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await _db.Animals.OrderBy(a => a.Name).Take(50).ToListAsync();

            query = query.ToLower();
            return await _db.Animals
                .Where(a => a.Name.ToLower().Contains(query))
                .OrderBy(a => a.Name)
                .Take(50)
                .ToListAsync();
        }

        public async Task UpdateAsync(Animal animal)
        {
            _db.Animals.Update(animal);
            await _db.SaveChangesAsync();
        }
    }
}
