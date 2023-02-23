using CarPool.Models.DBModels;
using CarPool.Models;
using CarPool.Models.ViewModel;

namespace CarPool.Interface
{
    public interface IBookARideService
    {
        public List<MatchingRide> GetAvailableRidesToBook(RideData bookRideData);
        public String BookARide(RideBookingRequest rideBookingData);
        public BookingCard GetBookingCard(int AvailableRideId, int FromLocationID, int ToLocationID);
    }
}
