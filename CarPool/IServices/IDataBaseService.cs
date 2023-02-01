using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.IServices
{
    public interface IDataBaseService
    {
        public Boolean SaveSignUpDataInUsers(User newUser);

        public User GetUser(LogInData logInData);
        public Boolean SaveInAvailableRides(AvailableRides offerRideData);
    }
}
