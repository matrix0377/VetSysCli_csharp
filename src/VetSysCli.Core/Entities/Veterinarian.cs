using System;

namespace VetSysCli.Core.Entities
{
    public class Veterinarian
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClinicName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
