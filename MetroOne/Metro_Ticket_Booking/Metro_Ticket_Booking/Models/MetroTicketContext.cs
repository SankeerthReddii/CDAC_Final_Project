using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // if needed for ServerVersion

namespace Metro_Ticket_Booking.Models
{
    public partial class MetroTicketContext : DbContext
    {
        public MetroTicketContext()
        {
        }

        public MetroTicketContext(DbContextOptions<MetroTicketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<Metro> Metros { get; set; }
        public virtual DbSet<MetroCard> MetroCards { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        // Optional: Commented out OnConfiguring if using Dependency Injection for DbContext setup
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=database-1.cv2qomuemf6s.eu-north-1.rds.amazonaws.com;database=MetroOne;user=admin;password=nimitesh123;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminId).HasName("PK_Admins");

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasKey(e => e.ComplaintId).HasName("PK_Complaints");

                entity.Property(e => e.RepliedAt)
                    .HasColumnType("datetime");

                entity.Property(e => e.SubmittedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaints_User");
            });

            modelBuilder.Entity<Metro>(entity =>
            {
                entity.HasKey(e => e.MetroId).HasName("PK_Metros");

                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<MetroCard>(entity =>
            {
                entity.HasKey(e => e.CardId).HasName("PK_MetroCards");

                entity.Property(e => e.ApplicationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.CardStatus)
                    .HasMaxLength(50)
                    .HasDefaultValue("Pending");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MetroCards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MetroCards_User");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId).HasName("PK_Payments");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValue("INR")
                    .IsFixedLength();

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(50);

                entity.Property(e => e.RazorpayOrderId)
                    .HasMaxLength(100);

                entity.Property(e => e.RazorpayPaymentId)
                    .HasMaxLength(100);

                entity.Property(e => e.RazorpaySignature)
                    .HasMaxLength(256);

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Ticket");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.RouteId).HasName("PK_Routes");

                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasOne(d => d.EndStation)
                    .WithMany(p => p.RouteEndStations)
                    .HasForeignKey(d => d.EndStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_EndStation");

                entity.HasOne(d => d.StartStation)
                    .WithMany(p => p.RouteStartStations)
                    .HasForeignKey(d => d.StartStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_StartStation");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(e => e.StationId).HasName("PK_Stations");

                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketId).HasName("PK_Tickets");

                entity.Property(e => e.BookingDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.FromStation)
                    .WithMany(p => p.TicketFromStations)
                    .HasForeignKey(d => d.FromStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_FromStation");

                entity.HasOne(d => d.Metro)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.MetroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Metro");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Route");

                entity.HasOne(d => d.ToStation)
                    .WithMany(p => p.TicketToStations)
                    .HasForeignKey(d => d.ToStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_ToStation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_Users");

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LoyaltyPoints)
                    .HasDefaultValue(0);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("longtext");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
