using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Metro_Ticket_Booking.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Sankeerth\\SQLEXPRESS;Initial Catalog=Metro_Ticket;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE48846DDBA69");

            entity.HasIndex(e => e.Email, "UQ__Admins__A9D10534EC0F95F6").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PK__Complain__740D898FC4E4CA0E");

            entity.Property(e => e.RepliedAt).HasColumnType("datetime");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Complaints_User");
        });

        modelBuilder.Entity<Metro>(entity =>
        {
            entity.HasKey(e => e.MetroId).HasName("PK__Metros__D2C599B7EDB394A9");

            entity.HasIndex(e => e.Name, "UQ__Metros__737584F66208B975").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<MetroCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__MetroCar__55FECDAED6F1D5B8");

            entity.Property(e => e.ApplicationDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.CardStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.User).WithMany(p => p.MetroCards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MetroCards_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A388D9E91B4");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("INR")
                .IsFixedLength();
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.RazorpayOrderId).HasMaxLength(100);
            entity.Property(e => e.RazorpayPaymentId).HasMaxLength(100);
            entity.Property(e => e.RazorpaySignature).HasMaxLength(256);

            entity.HasOne(d => d.Ticket).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Ticket");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Routes__80979B4D909FBD5C");

            entity.HasIndex(e => e.Name, "UQ__Routes__737584F67332D544").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.EndStation).WithMany(p => p.RouteEndStations)
                .HasForeignKey(d => d.EndStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Routes_EndStation");

            entity.HasOne(d => d.StartStation).WithMany(p => p.RouteStartStations)
                .HasForeignKey(d => d.StartStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Routes_StartStation");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__E0D8A6BD1AE658E6");

            entity.HasIndex(e => e.Name, "UQ__Stations__737584F66262B83A").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC607D952F408");

            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.FromStation).WithMany(p => p.TicketFromStations)
                .HasForeignKey(d => d.FromStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_FromStation");

            entity.HasOne(d => d.Metro).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MetroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Metro");

            entity.HasOne(d => d.Route).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Route");

            entity.HasOne(d => d.ToStation).WithMany(p => p.TicketToStations)
                .HasForeignKey(d => d.ToStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_ToStation");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C94AF2F6A");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534687E51F9").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LoyaltyPoints).HasDefaultValue(0);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
