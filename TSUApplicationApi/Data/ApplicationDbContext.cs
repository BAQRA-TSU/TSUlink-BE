﻿using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<SubjectReview> SubjectReviews { get; set; }
        public DbSet<LecturerReview> LecturerReviews { get; set; }
        public DbSet<LecturerSubject> LecturerSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LecturerSubject>()
                .HasKey(ls => new { ls.LecturerId, ls.SubjectId, ls.Type }); // 🔧 დამატებულია Type

            modelBuilder.Entity<LecturerSubject>()
                .HasOne(ls => ls.Lecturer)
                .WithMany(l => l.LecturerSubjects)
                .HasForeignKey(ls => ls.LecturerId);

            modelBuilder.Entity<LecturerSubject>()
                .HasOne(ls => ls.Subject)
                .WithMany(s => s.LecturerSubjects)
                .HasForeignKey(ls => ls.SubjectId);
            modelBuilder.Entity<SubjectReview>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId); // 🟢 FK ხელით გაწყვეტ
            modelBuilder.Entity<LecturerReview>()
             .HasOne(r => r.User)
             .WithMany()
             .HasForeignKey(r => r.UserId); // 🟢 FK ხელით გაწყვეტ
        }
    }
}
