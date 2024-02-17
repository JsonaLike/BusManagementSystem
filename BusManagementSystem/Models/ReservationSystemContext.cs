using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusManagementSystem.Models
{
    public partial class ReservationSystemContext : IdentityDbContext
    {
        public ReservationSystemContext()
        {
        }

        public ReservationSystemContext(DbContextOptions<ReservationSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bus> buses { get; set; } = null!;
        public virtual DbSet<BusTypeMapping> BusTypeMappings { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<GenderMapping> GenderMappings { get; set; } = null!;
        public virtual DbSet<LicenseTypeMapping> LicenseTypeMappings { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5P9ACNC\\SQLEXPRESS;Initial Catalog=ReservationSystem;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.HasKey(e => e.BusId)
                    .HasName("PK_Bus");

                entity.ToTable("bus");

                entity.Property(e => e.BusId)
                .ValueGeneratedOnAdd()
                    .HasColumnName("bus_id");

                entity.Property(e => e.BusType).HasColumnName("bus_type");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Model)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("model");

                entity.Property(e => e.PlateNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("plate_number");

                entity.HasOne(d => d.BusMapping)
                     .WithMany(p => p.Bus)
                     .HasForeignKey(d => d.BusType)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_bus_bus_type_mapping");
            });

            modelBuilder.Entity<BusTypeMapping>(entity =>
            {
                entity.ToTable("bus_type_mapping");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.BusType)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("bus_type");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.Property(e => e.DriverId)
                    .HasColumnName("Driver_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("email");

                entity.Property(e => e.LicenseExpiry)
                    .HasColumnType("date")
                    .HasColumnName("license_expiry");

                entity.Property(e => e.LicenseType).HasColumnName("license_type");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.HasOne(d => d.LicenseTypeNavigation)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.LicenseType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Driver_license_type_mapping");
            });

            modelBuilder.Entity<GenderMapping>(entity =>
            {
                entity.HasKey(e => e.GenderId);

                entity.ToTable("gender_mapping");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("gender_id");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .HasColumnName("gender");
            });

            modelBuilder.Entity<LicenseTypeMapping>(entity =>
            {
                entity.HasKey(e => e.LicenseTypeId)
                    .HasName("PK_License_type_mapping");

                entity.ToTable("license_type_mapping");

                entity.Property(e => e.LicenseTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("license_type_id");

                entity.Property(e => e.LicenseTypeName)
                    .HasMaxLength(250)
                    .HasColumnName("license_type_name");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("passenger");

                entity.Property(e => e.PassengerId)
                    .HasColumnName("passenger_id");

                entity.Property(e => e.user_id)
                .ValueGeneratedNever()
                .HasColumnName("user_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");
                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => new { e.PassengerId, e.TripId })
                    .HasName("PK_Key");

                entity.ToTable("ticket");

                entity.HasIndex(e => e.PassengerId, "IX_Ticket");

                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.Property(e => e.TripId).HasColumnName("trip_id");

                

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.PassengerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Passenger");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Trip");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("trip");

                entity.Property(e => e.TripId)
                    .HasColumnName("trip_id");

                entity.Property(e => e.ArrivalCity).HasColumnName("arrival_city");

                entity.Property(e => e.BusId).HasColumnName("bus_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.DepartureCity).HasColumnName("departure_city");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.Reserved).HasColumnName("reserved");
                entity.Property(e => e.Price).HasColumnName("ticket_price");

                entity.Property(e => e.StartingTime)
                    .HasColumnType("time(0)")
                    .HasColumnName("starting_time");

                entity.HasOne(d => d.ArrivalCityNavigation)
                    .WithMany(p => p.TripArrivalCityNavigations)
                    .HasForeignKey(d => d.ArrivalCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trip_City1");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.BusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trip_Bus");

                entity.HasOne(d => d.DepartureCityNavigation)
                    .WithMany(p => p.TripDepartureCityNavigations)
                    .HasForeignKey(d => d.DepartureCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trip_City");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trip_driver");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
