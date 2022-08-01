using DevTrackR.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevTrackR.API.Persistence
{
    public class DevTrackRContext : DbContext
    {
        public DevTrackRContext(DbContextOptions<DevTrackRContext> options) 
            : base(options)
        {
            /*
            // Exemplo do Construtor
            Packages = new List<Package>();
            */
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageUpdate> PackageUpdates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Package>(e => {

                // Exemplo do ToTable.
                //e.ToTable("tb_Package");

                e.HasKey(p => p.Id);

                e
                .HasMany(p => p.Updates)
                .WithOne()
                .HasForeignKey(pu => pu.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PackageUpdate>(e => {
                e.HasKey(p => p.Id);
            });
        }
    }
}