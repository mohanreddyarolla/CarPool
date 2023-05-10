using Microsoft.EntityFrameworkCore;

namespace Carpool.Models.DBModels
{
    public class CarPoolDBContext : DbContext
    {
        public CarPoolDBContext(DbContextOptions<CarPoolDBContext> options) : base(options) { }

        public DbSet<OfferedRide> OfferedRide { get; set; }
        public DbSet<AvailableSeats> AvailableSeats { get; set; }
        public DbSet<BookedRide> BookedRide { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
