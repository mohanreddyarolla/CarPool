using CarPool.Interface;
using CarPool.Interface.IRepository;
using Carpool.Models;
using Carpool.Models.DBModels;
using Carpool.Models.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CarPool.Services
{
    public class BookARideService:IBookARideService
    {
       
        IValidator validation;
        ILocationsRepository locationsRepository;
        IBookedRidesRepository bookedRidesRepository;
        IAvailableSeatsRepository availableSeatsRepository;
        IOfferedRidesRepository offeredRidesRepository;
        IUserRepository userRepository;

        public BookARideService(IValidator _validation,IUserRepository _userRepository, IBookedRidesRepository _bookedRidesRepository, IAvailableSeatsRepository _availableSeatsRepository, ILocationsRepository _locationsRepository, IOfferedRidesRepository _offeredRidesRepository)
        {
            
            validation = _validation;
            bookedRidesRepository = _bookedRidesRepository;
            availableSeatsRepository = _availableSeatsRepository;
            locationsRepository = _locationsRepository;
            offeredRidesRepository = _offeredRidesRepository;
            userRepository = _userRepository;
        }

        public async Task<List<MatchingRide>> GetAvailableRidesToBook(RideData rideData)
        {

            List<OfferedRide> rides = await offeredRidesRepository.GetAvailbleRides();

            List<MatchingRide> matchingRides = new List<MatchingRide>();

            foreach (OfferedRide ride in rides)
            {

                List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));

                if (validation.HasMatchingPickupAndDropoff(rideData.FromLocationId, rideData.ToLocationId, stopListIds) && ride.Date == rideData.Date)
                {
                    MatchingRide newMatch = new MatchingRide();

                    newMatch.RideID = ride.OfferedRideId;
                    newMatch.From = await locationsRepository.GetLocationById(rideData.FromLocationId);
                    newMatch.To = await locationsRepository.GetLocationById(rideData.ToLocationId);
                    newMatch.SeatAvailability = await GetMinimumSeatsAvailable(stopListIds, ride.OfferedRideId, stopListIds[0], stopListIds[stopListIds.Count - 1]);

                    if (newMatch.SeatAvailability <= 0) continue;
                    newMatch.Date = ride.Date;
                    newMatch.Price = ride.TotalPrice;
                    newMatch.Name = await userRepository.GetUserName(ride.RideProviderId);
                    newMatch.Time= ride.Time;

                    matchingRides.Add(newMatch);
                }
            }
            return matchingRides;
        }

        public async Task<int> GetMinimumSeatsAvailable(List<int> stopListIds, int rideId, int fromLocationId, int ToLocationId)
        {
            int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
            int ToLocationIndex = stopListIds.IndexOf(ToLocationId);
            int minSeats = 10;

            List<int> seatsAtEachStop = await availableSeatsRepository.GetAvailableSeatsList(rideId, stopListIds);

            for (int i = FromLocationIndex; i < ToLocationIndex; i++)
            {
                minSeats =  Math.Min(seatsAtEachStop[i], minSeats);
            }

            return minSeats;
        }

        public async Task<BookingCard> GetBookingCard(int AvailableRideId, int FromLocationID, int ToLocationID)
        {
            OfferedRide ride = await offeredRidesRepository.GetAvailableRidesById(AvailableRideId);
            BookingCard bookingCard = new BookingCard();

            List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));


            bookingCard.From = await locationsRepository.GetLocationById(FromLocationID);
            bookingCard.To = await locationsRepository.GetLocationById(ToLocationID);
            bookingCard.Time = ride.Time;
            bookingCard.Date= ride.Date;
            bookingCard.RequiredSeats = 0;
            bookingCard.AvailableSeats = await GetMinimumSeatsAvailable(stopListIds, ride.OfferedRideId, FromLocationID, ToLocationID);
            bookingCard.StopList = await GetAllStopNames(stopListIds,FromLocationID, ToLocationID);

            return bookingCard;

        }

        public async Task<List<string>> GetAllStopNames( List<int> stopListIds,int fromLocationId, int ToLocationId)
        {
            List<string> stopNames = new List<string>();

            int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
            int ToLocationIndex = stopListIds.IndexOf(ToLocationId);

            for(int i=FromLocationIndex; i<=ToLocationIndex; i++)
            {
             stopNames.Add( await locationsRepository.GetLocationById(stopListIds[i]));
            }

            return stopNames;


        }
        public async Task<String> BookARide(RideBookingRequest rideBookingData)
        {
            OfferedRide ride = await offeredRidesRepository.GetAvailableRidesById(rideBookingData.AvailableRideId);
            List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));

            if (await validation.HasRoomForPassengers(stopListIds, rideBookingData.AvailableRideId, rideBookingData.RequiredSeats, rideBookingData.FromLocationId, rideBookingData.ToLocationId))
            {
                if(await bookedRidesRepository.SaveInBookedRides(rideBookingData.UserId, ride, rideBookingData.FromLocationId, rideBookingData.ToLocationId, rideBookingData.RequiredSeats,ride.RideProviderId))
                {
                    if(await availableSeatsRepository.ReserveSeats(stopListIds,rideBookingData.AvailableRideId,rideBookingData.RequiredSeats,rideBookingData.FromLocationId,rideBookingData.ToLocationId))
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
