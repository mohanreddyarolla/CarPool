using Carpool.Models.DBModels;

namespace CarPool.Interface.IRepository
{
    public interface  IBookedRidesRepository
    {
        public Task<Boolean> SaveInBookedRides(int UserId, OfferedRide ride, int fromLocationId, int ToLocationId, int SeatsReserved, int ridePRoviderId);
        public Task<List<BookedRide>> GetAllBookedRidesByUserId(int userId);
    }
}
