using Microsoft.EntityFrameworkCore;
using VetSysCli.Core.Entities;

namespace VetSysCli.Infrastructure.Data
{
    public class VetSysDbContext : DbContext
    {
        public VetSysDbContext(DbContextOptions<VetSysDbContext> options) : base(options) { }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<Animal>().HasIndex(a => a.Name);
            base.OnModelCreating(modelBuilder);
        }
    }
}
