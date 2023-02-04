using CarPool.Models.DBModels;
using CarPool.Models;

namespace CarPool.IServices
{
    public interface IBookARideService
    {
        public List<OfferedRides> GetAvailableRidesToBook(RideData bookRideData);
        public String BookARide(RideBookingData rideBookingData);
    }
}
