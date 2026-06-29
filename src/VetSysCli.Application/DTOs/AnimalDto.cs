using System;

namespace VetSysCli.Application.DTOs
{
    public class AnimalDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Age { get; set; } // calculada
        public string PhotoPath { get; set; }
    }
}
