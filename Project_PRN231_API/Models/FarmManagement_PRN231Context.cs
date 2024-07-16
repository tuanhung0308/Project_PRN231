using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_PRN231_API.Models
{
    public partial class FarmManagement_PRN231Context : DbContext
    {
        public FarmManagement_PRN231Context()
        {
        }

        public FarmManagement_PRN231Context(DbContextOptions<FarmManagement_PRN231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CareProcess> CareProcesses { get; set; } = null!;
        public virtual DbSet<Crop> Crops { get; set; } = null!;
        public virtual DbSet<Harvesting> Harvestings { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Storage> Storages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string ConnectionStr = config.GetConnectionString("DB");

                optionsBuilder.UseSqlServer(ConnectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CareProcess>(entity =>
            {
                entity.Property(e => e.CareProcessId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Crop)
                    .WithMany(p => p.CareProcesses)
                    .HasForeignKey(d => d.CropId)
                    .HasConstraintName("FK__CareProce__CropI__3E52440B");

                entity.HasOne(d => d.PerformedByNavigation)
                    .WithMany(p => p.CareProcesses)
                    .HasForeignKey(d => d.PerformedBy)
                    .HasConstraintName("FK__CareProce__Perfo__3F466844");
            });

            modelBuilder.Entity<Crop>(entity =>
            {
                entity.Property(e => e.CropId).ValueGeneratedNever();

                entity.Property(e => e.ActualHarvestDate).HasColumnType("date");

                entity.Property(e => e.CropName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpectedHarvestDate).HasColumnType("date");

                entity.Property(e => e.PlantingDate).HasColumnType("date");
            });

            modelBuilder.Entity<Harvesting>(entity =>
            {
                entity.HasKey(e => e.HarvestId)
                    .HasName("PK__Harvesti__CF5B24EE7FC71F28");

                entity.ToTable("Harvesting");

                entity.Property(e => e.HarvestId).ValueGeneratedNever();

                entity.Property(e => e.HarvestDate).HasColumnType("date");

                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Crop)
                    .WithMany(p => p.Harvestings)
                    .HasForeignKey(d => d.CropId)
                    .HasConstraintName("FK__Harvestin__CropI__4222D4EF");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).ValueGeneratedNever();

                entity.Property(e => e.Data).HasColumnType("text");

                entity.Property(e => e.GeneratedDate).HasColumnType("date");

                entity.Property(e => e.ReportName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.Property(e => e.StorageId).ValueGeneratedNever();

                entity.Property(e => e.Condition)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.StorageDate).HasColumnType("date");

                entity.Property(e => e.StorageLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Harvest)
                    .WithMany(p => p.Storages)
                    .HasForeignKey(d => d.HarvestId)
                    .HasConstraintName("FK__Storage__Harvest__44FF419A");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
