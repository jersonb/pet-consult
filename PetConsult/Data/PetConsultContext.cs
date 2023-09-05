using Microsoft.EntityFrameworkCore;
using PetConsult.Models;

namespace PetConsult.Data
{
    public class PetConsultContext : DbContext
    {
        public PetConsultContext(DbContextOptions<PetConsultContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("petconsult");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pet> Pets { get; set; } = default!;
        public DbSet<Consult> Consults { get; set; } = default!;
    }
}