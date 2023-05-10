using CarPool.Interface.IRepository;
using Carpool.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Repository
{
    public class BookedRidesRepository:IBookedRidesRepository
    {
        CarPoolDBContext carPoolDBContext;

        public BookedRidesRepository(CarPoolDBContext _carPoolDBContext)
        {
            carPoolDBContext = _carPoolDBContext;

        }

        public async Task<Boolean> SaveInBookedRides(int UserId, OfferedRide ride, int fromLocationId, int ToLocationId, int SeatsReserved, int rideProviderId)
        {
            try
            {
                BookedRide newRide = new BookedRide();

                newRide.BookedUserId = UserId;
                newRide.ReservedSeats = SeatsReserved;
                newRide.StartPointId = fromLocationId;
                newRide.StopPointId = ToLocationId;
                newRide.Date = ride.Date;
                newRide.Time = ride.Time;
                newRide.Price = ride.TotalPrice;
                newRide.RideProviderId = rideProviderId;

                await carPoolDBContext.BookedRide.AddAsync(newRide);
                await carPoolDBContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }

        public async Task<List<BookedRide>> GetAllBookedRidesByUserId(int userId)
        {
            List<BookedRide> offeredRides = new List<BookedRide>();

            offeredRides = await carPoolDBContext.BookedRide.Where(ride => ride.BookedUserId == userId).ToListAsync();

            return offeredRides;
        }
    }
}
