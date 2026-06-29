using System;
using VetSysCli.Core.Enums;

namespace VetSysCli.Core.Entities
{
    public class Animal
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid? VeterinarianId { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public AnimalStatus Status { get; set; }
        public string PhysicalCharacteristics { get; set; }
        public string PhotoPath { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
