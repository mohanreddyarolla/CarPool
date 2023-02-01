using Microsoft.EntityFrameworkCore;

namespace CarPool.Models.DBModels
{
    public class CarPoolDBContext : DbContext
    {
        public CarPoolDBContext(DbContextOptions<CarPoolDBContext> options) : base(options) { }

        public DbSet<AvailableRides> AvailableRides { get; set; }
        public DbSet<AvailableSeats> AvailableSeats { get; set; }
        public DbSet<BookedRides> BookedRides { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
