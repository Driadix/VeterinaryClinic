using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Pets)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "Иванов Иван Иванович", PasswordHash = "placeholder_hash" },
            new User { Id = 2, Username = "Петров Петр Петрович", PasswordHash = "placeholder_hash" },
            new User { Id = 3, Username = "Сидорова Анна Викторовна", PasswordHash = "placeholder_hash" }
        );
        }
    }
}
