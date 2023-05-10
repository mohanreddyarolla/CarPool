using Carpool.Models.DBModels;

namespace CarPool.Interface.IRepository
{
    public interface IOfferedRidesRepository
    {
        public Task<int> SaveRideOffer(OfferedRide offerRideData);
        public Task MakeRideInActive(OfferedRide newRide);
        public Task<List<OfferedRide>> GetAvailbleRides();
        public Task<OfferedRide> GetAvailableRidesById(int id);
        public Task<List<OfferedRide>> GetAllOfferedRidesByUserId(int userId);

    }
}
