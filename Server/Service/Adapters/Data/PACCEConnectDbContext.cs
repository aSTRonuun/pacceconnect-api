using Data.ArticulatorData;
using Domain.ArticulatorDomain.Entities;
using Domain.ManagerDomain.Entities;
using Domain.UserDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PACCEConnectDbContext : DbContext
    {
        public PACCEConnectDbContext(DbContextOptions<PACCEConnectDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Articulator> Articulators { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticulatorConfiguration());

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Articulator>()
                .ToTable("Articulators");

            modelBuilder.Entity<Manager>()
                .ToTable("Managers");

            modelBuilder.Entity<Articulator>()
                .HasBaseType<User>();

            modelBuilder.Entity<Manager>()
                .HasBaseType<User>();
        }
    }
}
