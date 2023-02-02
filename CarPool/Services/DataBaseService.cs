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

        public int SaveInAvailableRides(AvailableRides newRide)
        {
           
            try
            {
                carPoolDBContext.AvailableRides.Add(newRide);
                carPoolDBContext.SaveChanges();
                return newRide.AvailableRideId;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return -1;
            }

        }

        public bool SaveInAvailableSeats(AvailableSeats newSeats)
        {
            try
            {
                carPoolDBContext.AvailableSeats.Add(newSeats); 
                carPoolDBContext.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                //should revoke the available ride inservsion or just use status false
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public void MakeRideInActive(AvailableRides newRide)
        {
            try
            {
                carPoolDBContext.AvailableRides.Update(newRide);
                carPoolDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //should revoke the available ride inservsion or just use status false
                Console.WriteLine($"{ex.Message}");
            }
        }

        public List<AvailableRides> GetAvailbleRides()
        {
            List<AvailableRides>  matchingRides = new List<AvailableRides>();

            foreach(AvailableRides ride in  carPoolDBContext.AvailableRides)
            {
                if(ride.CurrentState == "Active")
                {
                    matchingRides.Add(ride);
                    Console.WriteLine(ride.UserId);
                }
                    
            }


            return matchingRides;
        }
    }
}
