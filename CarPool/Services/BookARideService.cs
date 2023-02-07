using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CarPool.Services
{
    public class BookARideService:IBookARideService
    {
        IDataBaseService dataBaseService;
        IValidator validation;

        public BookARideService(IDataBaseService _dataBaseService,IValidator _validation)
        {
            this.dataBaseService = _dataBaseService;
            validation = _validation;
        }

        public List<OfferedRides> GetAvailableRidesToBook(RideData rideData)
        {

            List<OfferedRides> rides = dataBaseService.GetAvailbleRides();

            List<OfferedRides> matchingRides = new List<OfferedRides>();

            foreach (OfferedRides ride in rides)
            {

                List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));

                if (validation.HasMatchingPickupAndDropoff(rideData.FromLocationId, rideData.ToLocationId, stopListIds) && ride.Date == rideData.Date)
                {
                    matchingRides.Add(ride);
                }
            }
            return matchingRides;
        }

        public String BookARide(RideBookingRequest rideBookingData)
        {
            OfferedRides ride = dataBaseService.GetAvailableRidesById(rideBookingData.AvailableRideId);
            List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));

            if (validation.HasRoomForPassengers(stopListIds, rideBookingData.AvailableRideId, rideBookingData.RequiredSeats, rideBookingData.FromLocationId, rideBookingData.ToLocationId))
            {
                if(dataBaseService.SaveInBookedRides(rideBookingData.UserId, ride, rideBookingData.FromLocationId, rideBookingData.ToLocationId, rideBookingData.RequiredSeats,ride.RideProviderId))
                {
                    if(dataBaseService.ReserveSeats(stopListIds,rideBookingData.AvailableRideId,rideBookingData.RequiredSeats,rideBookingData.FromLocationId,rideBookingData.ToLocationId))
                    {
                        return "Congratulations! Your ride has been booked successfully.";
                    } 
                }

                return "Sorry, the booking was not successful. Please try again later or contact customer support for assistance.";
            }

            return "Sorry, the ride has less than the required seats. Please try booking a different ride or reduce the number of required seats.";
        }
    }
}
