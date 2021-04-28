using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Data.Models;

#nullable disable

namespace Data.Context
{
    public partial class UniversityDepartmentWorkloadContext : DbContext
    {
        public UniversityDepartmentWorkloadContext()
        {
        }

        public UniversityDepartmentWorkloadContext(DbContextOptions<UniversityDepartmentWorkloadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DepartmentWorkload> DepartmentWorkloads { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Workload> Workloads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<DepartmentWorkload>(entity =>
            {
                entity.ToTable("DepartmentWorkload");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.DepartmentWorkloads)
                    .HasForeignKey(d => d.SubjectId);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.DepartmentWorkloads)
                    .HasForeignKey(d => d.TeacherId);

                entity.HasOne(d => d.Workload)
                    .WithMany(p => p.DepartmentWorkloads)
                    .HasForeignKey(d => d.WorkloadId);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.GenderName).HasMaxLength(15);
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.Property(e => e.MaritalStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Gender).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaritalStatus).HasDefaultValueSql("((2))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.Gender);

                entity.HasOne(d => d.MaritalStatusNavigation)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.MaritalStatus);
            });

            modelBuilder.Entity<Workload>(entity =>
            {
                entity.Property(e => e.WorkloadName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
