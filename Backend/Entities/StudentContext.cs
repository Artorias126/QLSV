using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLSV.Entities
{
    public partial class StudentContext : DbContext
    {
        public StudentContext()
        {
        }

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assessment> Bangtongkets { get; set; } = null!;
        public virtual DbSet<Faculty> Khoas { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Class> Lops { get; set; } = null!;
        public virtual DbSet<Student> Sinhviens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DUYNGUYEN\\SQLEXPRESS;Initial Catalog=sm;Integrated Security=True");
            }
        }
        public DbSet<Login> Users => Set<Login>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.HasKey(e => e.StudentID);

                entity.ToTable("ASSESSMENT");

                entity.Property(e => e.StudentID)
                    .ValueGeneratedNever()
                    .HasColumnName("StudentID");

                entity.Property(e => e.FacultyName).HasMaxLength(255);

                entity.Property(e => e.ClassName).HasMaxLength(255);

                entity.Property(e => e.StudentName)
                    .HasMaxLength(255)
                    .HasColumnName("StudentName");

                entity.Property(e => e.ScoreRating).HasMaxLength(2);

            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.FacultyID);

                entity.ToTable("FACULTY");

                entity.Property(e => e.FacultyName).HasMaxLength(255);

                entity.Property(e => e.FacultyID)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOGIN");

                entity.Property(e => e.Login1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Login");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity <Class>(entity =>
            {
                entity.HasKey(e => e.ClassID);
                   
                entity.ToTable("Class");

                entity.Property(e => e.ClassName).HasMaxLength(255);

                entity.Property(e => e.ClassID)
                    .HasMaxLength(4)
                    .HasColumnName("ClassID");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentID);

                entity.ToTable("STUDENTS");

                entity.Property(e => e.StudentID)
                    .ValueGeneratedNever()
                    .HasColumnName("StudentID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.FacultyName).HasMaxLength(255);

                entity.Property(e => e.ClassName).HasMaxLength(255);

                entity.Property(e => e.StudentName)
                    .HasMaxLength(255)
                    .HasColumnName("StudentName");

               
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
