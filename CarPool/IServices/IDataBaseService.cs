using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.IServices
{
    public interface IDataBaseService
    {
        public Boolean SaveUserSignUpDetails(User newUser);

        public User FetchUserData(LogInData logInData);
        public int SaveRideOffer(OfferedRides offerRideData);
        public bool SaveInAvailableSeats(AvailableSeats newSeats);

        public void MakeRideInActive(OfferedRides newRide);

        public List<OfferedRides> GetAvailbleRides();
        public List<int> GetAvailableSeatsList(int availableRideId, List<int> stopListIds);
        public OfferedRides GetAvailableRidesById(int id);
        public Boolean SaveInBookedRides(int UserId, OfferedRides ride, int fromLocationId, int ToLocationId, int SeatsReserved);
        public Boolean ReserveSeats(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);
        public List<OfferedRides> GetAllOfferedRidesByUserId(int userId);
        public List<BookedRides> GetAllBookedRidesByUserId(int userId);
    }
}
