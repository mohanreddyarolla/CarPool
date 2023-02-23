using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.Interface
{
    public interface IDataBaseService
    {
        public int SaveUserSignUpDetails(User newUser);

        public User FetchUserData(LogInRequest logInData);
        public int SaveRideOffer(OfferedRides offerRideData);
        public bool SaveInAvailableSeats(AvailableSeats newSeats);

        public void MakeRideInActive(OfferedRides newRide);

        public List<OfferedRides> GetAvailbleRides();
        public List<int> GetAvailableSeatsList(int availableRideId, List<int> stopListIds);
        public OfferedRides GetAvailableRidesById(int id);
        public Boolean SaveInBookedRides(int UserId, OfferedRides ride, int fromLocationId, int ToLocationId, int SeatsReserved, int ridePRoviderId);
        public Boolean ReserveSeats(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);
        public List<OfferedRides> GetAllOfferedRidesByUserId(int userId);
        public List<BookedRides> GetAllBookedRidesByUserId(int userId);
        public IEnumerable<String> GetAllUserNames();
        public int GetUserId(string EmailId);
        public List<Locations> GetLocations();
        public string GetUserName(int userId);
        public int GetAvailableSeats(int AvailableRideId, int LocationId);
        public string GetLocationById(int id);



    }
}
