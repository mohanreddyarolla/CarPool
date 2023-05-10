using Carpool.Models.DBModels;

namespace CarPool.Interface.IRepository
{
    public interface IAvailableSeatsRepository
    {
        public Task<bool> SaveInAvailableSeats(AvailableSeats newSeats);
        public Task<List<int>> GetAvailableSeatsList(int availableRideId, List<int> stopListIds);
        public Task<int> GetAvailableSeats(int AvailableRideId, int LocationId);
        public Task<Boolean> ReserveSeats(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);

    }
}
