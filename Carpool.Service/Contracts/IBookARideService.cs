using Carpool.Models.DBModels;
using Carpool.Models;
using Carpool.Models.ViewModel;

namespace CarPool.Interface
{
    public interface IBookARideService
    {
        public Task<List<MatchingRide>> GetAvailableRidesToBook(RideData bookRideData);
        public Task<String> BookARide(RideBookingRequest rideBookingData);
        public Task<BookingCard> GetBookingCard(int AvailableRideId, int FromLocationID, int ToLocationID);
    }
}
