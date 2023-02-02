using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.IServices
{
    public interface IDataBaseService
    {
        public Boolean SaveSignUpDataInUsers(User newUser);

        public User GetUser(LogInData logInData);
        public int SaveInAvailableRides(AvailableRides offerRideData);
        public bool SaveInAvailableSeats(AvailableSeats newSeats);

        public void MakeRideInActive(AvailableRides newRide);

        public List<AvailableRides> GetAvailbleRides();
    }
}
