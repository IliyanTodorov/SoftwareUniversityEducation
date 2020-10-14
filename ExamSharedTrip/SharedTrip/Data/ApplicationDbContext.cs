namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }

        public ApplicationDbContext()
	    {
           
	    }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString, s => s.MigrationsAssembly("SharedTrip"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>()
            .HasKey(t => new { t.UserId, t.TripId });

            modelBuilder.Entity<UserTrip>()
                .HasOne(ut => ut.User)
                .WithMany(ut => ut.Trips)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTrip>()
                .HasOne(ut => ut.Trip)
                .WithMany(ut => ut.Users)
                .HasForeignKey(ut => ut.TripId);
        }
    }
}
