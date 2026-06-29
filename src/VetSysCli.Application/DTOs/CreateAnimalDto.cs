using System;

namespace VetSysCli.Application.DTOs
{
    public class CreateAnimalDto
    {
        public Guid OwnerId { get; set; }
        public Guid? VeterinarianId { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhysicalCharacteristics { get; set; }
        public string Notes { get; set; }
    }
}
