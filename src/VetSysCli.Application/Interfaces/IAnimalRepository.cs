using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetSysCli.Core.Entities;

namespace VetSysCli.Application.Interfaces
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> SearchAsync(string query);
        Task<Animal> GetByIdAsync(Guid id);
        Task<Animal> AddAsync(Animal animal);
        Task UpdateAsync(Animal animal);
    }
}
