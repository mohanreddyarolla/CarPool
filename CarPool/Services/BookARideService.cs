using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
using CarPool.Models.ViewModel;
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

        public List<MatchingRide> GetAvailableRidesToBook(RideData rideData)
        {

            List<OfferedRides> rides = dataBaseService.GetAvailbleRides();

            List<MatchingRide> matchingRides = new List<MatchingRide>();

            foreach (OfferedRides ride in rides)
            {

                List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));

                if (validation.HasMatchingPickupAndDropoff(rideData.FromLocationId, rideData.ToLocationId, stopListIds) && ride.Date == rideData.Date)
                {
                    MatchingRide newMatch = new MatchingRide();

                    newMatch.RideID = ride.OfferedRideId;
                    newMatch.From = dataBaseService.GetLocationById(rideData.FromLocationId);
                    newMatch.To = dataBaseService.GetLocationById(rideData.ToLocationId);
                    newMatch.SeatAvailability = GetMinimumSeatsAvailable(stopListIds, ride.OfferedRideId, stopListIds[0], stopListIds[stopListIds.Count - 1]);
                    newMatch.Date = ride.Date;
                    newMatch.Price = ride.TotalPrice;
                    newMatch.Name = dataBaseService.GetUserName(ride.RideProviderId);
                    newMatch.Time= ride.Time;

                    matchingRides.Add(newMatch);
                }
            }
            return matchingRides;
        }

        public int GetMinimumSeatsAvailable(List<int> stopListIds, int rideId, int fromLocationId, int ToLocationId)
        {
            int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
            int ToLocationIndex = stopListIds.IndexOf(ToLocationId);
            int minSeats = 10;

            List<int> seatsAtEachStop = dataBaseService.GetAvailableSeatsList(rideId, stopListIds);

            for (int i = FromLocationIndex; i < ToLocationIndex; i++)
            {
                minSeats =  Math.Min(seatsAtEachStop[i], minSeats);
            }

            return minSeats;
        }

        public BookingCard GetBookingCard(int AvailableRideId, int FromLocationID, int ToLocationID)
        {
            OfferedRides ride = dataBaseService.GetAvailableRidesById(AvailableRideId);
            BookingCard bookingCard = new BookingCard();

            List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));


            bookingCard.From = dataBaseService.GetLocationById(FromLocationID);
            bookingCard.To = dataBaseService.GetLocationById(ToLocationID);
            bookingCard.Time = ride.Time;
            bookingCard.Date= ride.Date;
            bookingCard.RequiredSeats = 0;
            bookingCard.AvailableSeats = GetMinimumSeatsAvailable(stopListIds, ride.OfferedRideId, FromLocationID, ToLocationID);
            bookingCard.StopList = GetAllStopNames(stopListIds,FromLocationID, ToLocationID);

            return bookingCard;

        }

        public List<string> GetAllStopNames( List<int> stopListIds,int fromLocationId, int ToLocationId)
        {
            List<string> stopNames = new List<string>();

            int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
            int ToLocationIndex = stopListIds.IndexOf(ToLocationId);

            for(int i=FromLocationIndex; i<=ToLocationIndex; i++)
            {
             stopNames.Add( dataBaseService.GetLocationById(stopListIds[i]));
            }

            return stopNames;


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
