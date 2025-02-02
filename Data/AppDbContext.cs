using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) { }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Study> Study { get; set; }
        public DbSet<ApplicationConfig> ApplicationConfig { get; set; }


        public DbSet<StudyReview> StudyReview { get; set; }
        public DbSet<StudyPC> StudyPC { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure IdentityUserLogin composite key
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(u => new { u.LoginProvider, u.ProviderKey });

            // Material configuration
            modelBuilder.Entity<Material>()
                .HasKey(m => m.IdMaterial);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Topic)
                .WithMany(t => t.Materials)
                .HasForeignKey(m => m.IdTopic)
                .OnDelete(DeleteBehavior.Cascade);

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

            // Study configuration
            modelBuilder.Entity<Study>()
                .HasKey(s => s.IdStudy);

            modelBuilder.Entity<Study>()
                .Property(s => s.IdStudy)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Study>()
                .HasOne(s => s.Topic)
                .WithMany(t => t.Studies)
                .HasForeignKey(s => s.IdTopic)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Study>()
                .HasOne(s => s.User)
                .WithMany(u => u.Studies)
                .HasForeignKey(s => s.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Study>()
                .HasOne(s => s.StudyPC)
                .WithMany(u => u.Studies)
                .HasForeignKey(s => s.IdStudyPC)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Study>()
                .Property(m => m.OperationDate)
                .IsRequired(true);


            modelBuilder.Entity<Topic>()
            .HasKey(s => s.Id);

            modelBuilder.Entity<Topic>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Topic>()
         .Property(m => m.OperationDate)
         .IsRequired(true);

            modelBuilder.Entity<Topic>()
          .HasOne(s => s.User)
          .WithMany(u => u.Topics)
          .HasForeignKey(s => s.IdUser)
          .OnDelete(DeleteBehavior.Cascade);

            // Study configuration
            modelBuilder.Entity<ApplicationConfig>()
                .HasKey(s => s.IdApplicationConfig);


            modelBuilder.Entity<ApplicationConfig>()
                .Property(s => s.IdApplicationConfig)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<ApplicationConfig>()
            .Property(m => m.OperationDate)
            .IsRequired(true);

            modelBuilder.Entity<ApplicationConfig>()
            .Property(m => m.Name)
            .IsRequired(true);

            modelBuilder.Entity<ApplicationConfig>()
            .Property(m => m.JsonContent)
            .IsRequired(true);

            // StudyReview configuration

            modelBuilder.Entity<StudyReview>()
            .HasKey(s => s.IdStudyReview);

            modelBuilder.Entity<StudyReview>()
                .Property(s => s.IdStudyReview)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StudyReview>()
            .Property(m => m.IdStudy)
            .IsRequired(true);

            modelBuilder.Entity<StudyReview>()
            .Property(m => m.IdStudyPC)
            .IsRequired(true);

            modelBuilder.Entity<StudyReview>()
            .Property(m => m.OperationDate)
            .IsRequired(true);

            modelBuilder.Entity<StudyReview>()
              .HasOne(s => s.StudyPC)
              .WithMany(u => u.StudyReviews)
              .HasForeignKey(s => s.IdStudyPC)
              .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<StudyReview>()
              .HasOne(s => s.Study)
              .WithMany(u => u.StudyReviews)
              .HasForeignKey(s => s.IdStudy)
              .OnDelete(DeleteBehavior.Cascade);


            //// StudyPC configuration

            modelBuilder.Entity<StudyPC>()
                .HasKey(s => s.IdStudyPC);

            modelBuilder.Entity<StudyPC>()
                .Property(s => s.IdStudyPC)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StudyPC>()
                .Property(m => m.Name)
                .IsRequired(true);



        }
    }
}