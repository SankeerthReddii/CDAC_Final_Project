using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Route = Metro_Ticket_Project.Models.Entities.Route;

namespace Metro_Ticket_Project.Infrastructure.Data
{
    public class MetroDbContext : DbContext
    {
        public MetroDbContext(DbContextOptions<MetroDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<MetroCard> MetroCards { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Fare> Fares { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteDetails> RouteDetails { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripDetails> TripDetails { get; set; }
        public DbSet<BookingHistory> BookingHistories { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(15);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.HasIndex(e => e.Email).IsUnique();

                // One-to-One relationship
                entity.HasOne(u => u.Card)
                      .WithOne(c => c.User)
                      .HasForeignKey<MetroCard>(c => c.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // Configure Admin entity
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configure MetroCard entity
            modelBuilder.Entity<MetroCard>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CardNo).IsRequired().HasMaxLength(16);
                entity.HasIndex(e => e.CardNo).IsUnique();
                entity.Property(e => e.Balance).HasColumnType("decimal(10,2)").HasDefaultValue(0);
                entity.Property(e => e.CardStatus).HasDefaultValue(false);
                entity.Property(e => e.Pin);
                entity.Property(e => e.ICard);
                entity.Property(e => e.ICardNo).HasMaxLength(100);
            });


            // Configure Station entity
            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Location).HasMaxLength(200);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            // Configure Fare entity
            modelBuilder.Entity<Fare>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(8,2)").IsRequired();
                entity.Property(e => e.Distance).HasColumnType("decimal(6,2)");

                // Foreign keys to Stations
                entity.HasOne(e => e.Source)
                      .WithMany()
                      .HasForeignKey(e => e.FromStationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Destination)
                      .WithMany()
                      .HasForeignKey(e => e.ToStationId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Unique constraint for route combination
                entity.HasIndex(e => new { e.FromStationId, e.ToStationId }).IsUnique();
            });

            // Configure Route entity
            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RouteName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RouteCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.TotalDistance).HasColumnType("decimal(8,2)");
                entity.HasIndex(e => e.RouteCode).IsUnique();
            });

            // Configure RouteDetails entity
            modelBuilder.Entity<RouteDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StationOrder).IsRequired();
                entity.Property(e => e.DistanceFromPrevious).HasColumnType("decimal(6,2)");

                // Foreign keys
                entity.HasOne(e => e.Route)
                      .WithMany(r => r.RouteDetails)
                      .HasForeignKey(e => e.RouteId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Station)
                      .WithMany()
                      .HasForeignKey(e => e.StationId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Unique constraint for route and station order
                entity.HasIndex(e => new { e.RouteId, e.StationOrder }).IsUnique();
            });

            // Configure Train entity
            modelBuilder.Entity<Train>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TrainNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.TrainName).HasMaxLength(100);
                entity.Property(e => e.Capacity).HasDefaultValue(300);
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Active");
                entity.HasIndex(e => e.TrainNumber).IsUnique();
            });

            // Configure Trip entity
            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TripCode).IsRequired().HasMaxLength(20);
                entity.Property(e => e.DepartureTime).IsRequired();
                entity.Property(e => e.ArrivalTime).IsRequired();
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Scheduled");

                // Foreign keys
                entity.HasOne(e => e.Train)
                      .WithMany()
                      .HasForeignKey(e => e.TrainId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Route)
                      .WithMany()
                      .HasForeignKey(e => e.RouteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure TripDetails entity
            modelBuilder.Entity<TripDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ArrivalTime);
                entity.Property(e => e.DepartureTime);
                entity.Property(e => e.StationOrder).IsRequired();

                // Foreign keys
                entity.HasOne(e => e.Trip)
                      .WithMany(t => t.TripDetails)
                      .HasForeignKey(e => e.TripId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Station)
                      .WithMany()
                      .HasForeignKey(e => e.StationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Complaint entity
            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.Priority).HasMaxLength(10).HasDefaultValue("Medium");

                // Foreign key to User
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Reply entity
            modelBuilder.Entity<Reply>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReplyText).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.RepliedBy).HasMaxLength(100);

                // Foreign key to Complaint
                entity.HasOne(e => e.Complaint)
                      .WithMany(c => c.Replies)
                      .HasForeignKey(e => e.ComplaintId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Foreign key to Admin
                entity.HasOne(e => e.Admin)
                      .WithMany()
                      .HasForeignKey(e => e.AdminId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure BookingHistory entity
            modelBuilder.Entity<BookingHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TicketId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Amount).HasColumnType("decimal(8,2)").IsRequired();
                entity.Property(e => e.PaymentMethod).HasMaxLength(20);
                entity.Property(e => e.PaymentStatus).HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.TicketStatus).HasMaxLength(20).HasDefaultValue("Booked");

                // Foreign keys
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.FromStation)
                      .WithMany()
                      .HasForeignKey(e => e.FromStationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ToStation)
                      .WithMany()
                      .HasForeignKey(e => e.ToStationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure History entity
            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TransactionType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Status).HasMaxLength(20);

                // Foreign key to User
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Optional foreign key to MetroCard
                entity.HasOne(e => e.MetroCard)
                      .WithMany()
                      .HasForeignKey(e => e.MetroCardId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure indexes for better performance
            modelBuilder.Entity<BookingHistory>()
                .HasIndex(e => e.BookingDate)
                .HasDatabaseName("IX_BookingHistory_BookingDate");

            modelBuilder.Entity<History>()
                .HasIndex(e => e.TransactionDate)
                .HasDatabaseName("IX_History_TransactionDate");

            modelBuilder.Entity<TripDetails>()
                .HasIndex(e => new { e.TripId, e.StationOrder })
                .HasDatabaseName("IX_TripDetails_Trip_StationOrder");

            // Add any additional configurations
            ConfigureDecimalPrecision(modelBuilder);
            ConfigureDateTimeDefaults(modelBuilder);
        }

        private static void ConfigureDecimalPrecision(ModelBuilder modelBuilder)
        {
            // Configure decimal precision for all decimal properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        if (property.GetColumnType() == null) // Only set if not already configured
                        {
                            property.SetColumnType("decimal(18,2)");
                        }
                    }
                }
            }
        }

        private static void ConfigureDateTimeDefaults(ModelBuilder modelBuilder)
        {
            // Set default values for CreatedDate and UpdatedDate
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var createdDateProperty = entityType.FindProperty("CreatedDate");

                if (createdDateProperty != null)
                {
                    createdDateProperty.SetDefaultValueSql("GETDATE()");
                }

                var updatedDateProperty = entityType.FindProperty("UpdatedDate");
                if (updatedDateProperty != null)
                {
                    updatedDateProperty.SetDefaultValueSql("GETDATE()");
                }
            }
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = DateTime.Now;
                    entity.UpdatedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedDate = DateTime.Now;
                    // Prevent CreatedDate from being updated
                    entry.Property(nameof(BaseEntity.CreatedDate)).IsModified = false;
                }
            }
        }
    }
}

