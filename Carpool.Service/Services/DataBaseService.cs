using CarPool.Interface;
using Carpool.Models;
using Carpool.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarPool.Services
{
    public class DataBaseService:IDataBaseService
    {
        CarPoolDBContext carPoolDBContext;
       
        public DataBaseService(CarPoolDBContext _carPoolDBContext) 
        {
            carPoolDBContext = _carPoolDBContext;
           
        }

        public int SaveUserSignUpDetails(User newUser)
        {
            try
            {
                carPoolDBContext.Users.Add(newUser);
                carPoolDBContext.SaveChanges();
                return newUser.UserId;
            }
            catch(Exception ex)
            {
                return -1;
            }
            
        }

        public User FetchUserData(LogInRequest logInData)
        {
            var userData = carPoolDBContext.Users.FirstOrDefault(user => user.EmailId == logInData.EmailId && user.Password == logInData.Password);

            return userData;
        }

        public int SaveRideOffer(OfferedRide newRide)
        {
           
            try
            {
                carPoolDBContext.OfferedRide.Add(newRide);
                carPoolDBContext.SaveChanges();
                return newRide.OfferedRideId;
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

        public void MakeRideInActive(OfferedRide newRide)
        {
            try
            {
                newRide.CurrentState = "InActive";
                carPoolDBContext.OfferedRide.Update(newRide);
                carPoolDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //should revoke the available ride inservsion or just use status false
                Console.WriteLine($"{ex.Message}");
            }
        }

        public List<OfferedRide> GetAvailbleRides()
        {
            List<OfferedRide>  matchingRides = new List<OfferedRide>();

            try
            {
                foreach (OfferedRide ride in carPoolDBContext.OfferedRide)
                {
                    if (ride.CurrentState == "Active")
                    {
                        matchingRides.Add(ride);
                        Console.WriteLine(ride.RideProviderId);
                    }

                }
            }
            catch(Exception ex)
            {

            }


            return matchingRides;
        }
        
        public List<int> GetAvailableSeatsList(int availableRideId,List<int> stopListIds)
        {
            List<int> availableSeats = new List<int>();

            try
            {
                foreach (int id in stopListIds)
                {
                    var seats = carPoolDBContext.AvailableSeats.FirstOrDefault(seat => seat.AvailableRideId == availableRideId && seat.LocationId == id);
                    if (seats != null)
                        availableSeats.Add(seats.SeatAvailability);
                }
            }
            catch(Exception ex)
            {

            }

                 
            
            return availableSeats;
        }

        public OfferedRide GetAvailableRidesById(int id)
        {
            OfferedRide offeredRides = new OfferedRide();

            try
            {
                offeredRides = carPoolDBContext.OfferedRide.FirstOrDefault(ride => ride.OfferedRideId == id);

            }
            catch(Exception ex) { }


             return offeredRides;
        }

        public Boolean SaveInBookedRides(int UserId,OfferedRide ride, int fromLocationId, int ToLocationId,int SeatsReserved, int rideProviderId)
        {
            try
            {
                BookedRide newRide = new BookedRide();

                newRide.BookedUserId = UserId;
                newRide.ReservedSeats = SeatsReserved;
                newRide.StartPointId = fromLocationId;
                newRide.StopPointId = ToLocationId;
                newRide.Date = ride.Date;
                newRide.Time = ride.Time;
                newRide.Price = ride.TotalPrice;
                newRide.RideProviderId = rideProviderId;

                carPoolDBContext.BookedRide.Add(newRide);
                carPoolDBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            

        }

        public Boolean ReserveSeats(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId)
        {
            try
            {
                int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
                int ToLocationIndex = stopListIds.IndexOf(ToLocationId);

                for (int i = FromLocationIndex; i < ToLocationIndex; i++)
                {
                    AvailableSeats seats = carPoolDBContext.AvailableSeats.FirstOrDefault(seats => seats.AvailableRideId == rideId && seats.LocationId == stopListIds[i]);

                    if (seats != null)
                    {
                        seats.SeatAvailability -= requiredSeats;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                carPoolDBContext.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            

        }

        public List<OfferedRide> GetAllOfferedRidesByUserId(int userId)
        {
            List<OfferedRide> offeredRides = new List<OfferedRide>();

            offeredRides = carPoolDBContext.OfferedRide.Where(ride => ride.RideProviderId == userId).ToList();

            return offeredRides;
        }

        public List<BookedRide> GetAllBookedRidesByUserId(int userId)
        {
            List<BookedRide> offeredRides = new List<BookedRide>();

            offeredRides = carPoolDBContext.BookedRide.Where(ride => ride.BookedUserId == userId).ToList();

            return offeredRides;
        }

        public IEnumerable<String> GetAllUserNames()
        {
            List<string> userNames = carPoolDBContext.Users.Select(user => user.EmailId).ToList();

            foreach(string userName in userNames)
            {
                yield return userName;
            }
        }

        public int GetUserId(string EmailId)
        {
            User user = carPoolDBContext.Users.FirstOrDefault(user => user.EmailId == EmailId);

            return user.UserId;
        }

        public List<Location> GetLocations()
        {
            List<Location> locations = carPoolDBContext.Location.ToList();

            return locations;
        }

        public string GetLocationById(int id)
        {
            string name = carPoolDBContext.Location.FirstOrDefault(location => location.LocationId == id).LocationName;
            if(name != "null")
            {
                return name;
            }
            return "";

        }

         public string GetUserName(int userId)
        {
            var user =  carPoolDBContext.Users.FirstOrDefault(user=>
            user.UserId == userId);
            
            return user.Name;
        }

        public int GetAvailableSeats(int AvailableRideId, int LocationId)
        {
            int seats = carPoolDBContext.AvailableSeats.FirstOrDefault(seat => seat.LocationId == LocationId && seat.AvailableRideId==AvailableRideId).SeatAvailability ;
            return seats;
        }

        public User GetUserData(int userId)
        {
            try
            {
                User user = carPoolDBContext.Users.FirstOrDefault(user => user.UserId == userId);

                return user;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public string UpdateUserData( User user)
        {
            try
            {
                carPoolDBContext.Users.Update(user);
                carPoolDBContext.SaveChanges();

                return "Updated Succefully!";
            }
            catch(Exception e)
            {
                return "Update UNSuccessful";
            }
        }

    }
}
