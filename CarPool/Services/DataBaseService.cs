using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.Services
{
    public class DataBaseService:IDataBaseService
    {
        CarPoolDBContext carPoolDBContext;
        public DataBaseService(CarPoolDBContext _carPoolDBContext) 
        {
            carPoolDBContext = _carPoolDBContext;
        }

        public Boolean SaveSignUpDataInUsers(User newUser)
        {
            
            carPoolDBContext.Users.Add(newUser);
            carPoolDBContext.SaveChanges();
            return true;
        }

        public User GetUser(LogInData logInData)
        {
            var userData = carPoolDBContext.Users.FirstOrDefault(user => user.EmailId == logInData.EmailId && user.Password == logInData.Password);

            return userData;
        }

        public Boolean SaveInAvailableRides(AvailableRides newRide)
        {
            Console.WriteLine("InDb");
            try
            {
                carPoolDBContext.AvailableRides.Add(newRide);
                carPoolDBContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
                
            
            return true;

        }
    }
}
