using Carpool.Models;
using Carpool.Models.DBModels;

namespace CarPool.Interface
{
    public interface IDataBaseService
    {
        public int SaveUserSignUpDetails(User newUser);
        public User FetchUserData(LogInRequest logInData);
        public int SaveRideOffer(OfferedRide offerRideData);
        public bool SaveInAvailableSeats(AvailableSeats newSeats);

        public void MakeRideInActive(OfferedRide newRide);

        public List<OfferedRide> GetAvailbleRides();
        public List<int> GetAvailableSeatsList(int availableRideId, List<int> stopListIds);
        public OfferedRide GetAvailableRidesById(int id);
        public Boolean SaveInBookedRides(int UserId, OfferedRide ride, int fromLocationId, int ToLocationId, int SeatsReserved, int ridePRoviderId);
        public Boolean ReserveSeats(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);
        public List<OfferedRide> GetAllOfferedRidesByUserId(int userId);
        public List<BookedRide> GetAllBookedRidesByUserId(int userId);
        public IEnumerable<String> GetAllUserNames();
        public int GetUserId(string EmailId);
        public List<Location> GetLocations();
        public string GetUserName(int userId);
        public int GetAvailableSeats(int AvailableRideId, int LocationId);
        public string GetLocationById(int id);
        public User GetUserData(int userId);
        public string UpdateUserData(User user);



    }
}
