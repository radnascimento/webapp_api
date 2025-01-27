using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Study> Studies { get; set; } // Add the DbSet for Study


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(u => new { u.LoginProvider, u.ProviderKey }); // IdentityUserLogin requires composite key


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Material>()
               .HasKey(m => m.IdMaterial);

            // Material - Topic (One-to-Many)
            modelBuilder.Entity<Material>()
                .HasOne(m => m.Topic)
                .WithMany(t => t.Materials)
                .HasForeignKey(m => m.IdTopic)
                .OnDelete(DeleteBehavior.Cascade); // Cascades deletion of materials when a topic is deleted

            modelBuilder.Entity<Material>()
            .Property(s => s.IdMaterial)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Material>()
              .HasOne(m => m.Level)
              .WithMany(l => l.Materials)
              .HasForeignKey(m => m.IdLevel)
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Material>()
            .Property(m => m.OperationDate)
            .IsRequired(true);


            modelBuilder.Entity<Study>()
               .HasKey(s => s.IdStudy);

            modelBuilder.Entity<Study>()
            .Property(s => s.IdStudy)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Study>()
               .HasOne(s => s.Topic) // Study has one Topic
               .WithMany(t => t.Studies) // Topic has many Studies
               .HasForeignKey(s => s.IdTopic)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Study>()
              .Property(m => m.OperationDate)
              .IsRequired(true);

        }
    }
}