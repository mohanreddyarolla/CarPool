using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
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

        public int SaveRideOffer(OfferedRides newRide)
        {
           
            try
            {
                carPoolDBContext.OfferedRides.Add(newRide);
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

        public void MakeRideInActive(OfferedRides newRide)
        {
            try
            {
                newRide.CurrentState = "InActive";
                carPoolDBContext.OfferedRides.Update(newRide);
                carPoolDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //should revoke the available ride inservsion or just use status false
                Console.WriteLine($"{ex.Message}");
            }
        }

        public List<OfferedRides> GetAvailbleRides()
        {
            List<OfferedRides>  matchingRides = new List<OfferedRides>();

            try
            {
                foreach (OfferedRides ride in carPoolDBContext.OfferedRides)
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

        public OfferedRides GetAvailableRidesById(int id)
        {
            OfferedRides offeredRides = new OfferedRides();

            try
            {
                offeredRides = carPoolDBContext.OfferedRides.FirstOrDefault(ride => ride.OfferedRideId == id);

            }
            catch(Exception ex) { }


             return offeredRides;
        }

        public Boolean SaveInBookedRides(int UserId,OfferedRides ride, int fromLocationId, int ToLocationId,int SeatsReserved, int rideProviderId)
        {
            try
            {
                BookedRides newRide = new BookedRides();

                newRide.BookedUserId = UserId;
                newRide.ReservedSeats = SeatsReserved;
                newRide.StartPointId = fromLocationId;
                newRide.StopPointId = ToLocationId;
                newRide.Date = ride.Date;
                newRide.Time = ride.Time;
                newRide.Price = ride.TotalPrice;
                newRide.RideProviderId = rideProviderId;

                carPoolDBContext.BookedRides.Add(newRide);
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

        public List<OfferedRides> GetAllOfferedRidesByUserId(int userId)
        {
            List<OfferedRides> offeredRides = new List<OfferedRides>();

            offeredRides = carPoolDBContext.OfferedRides.Where(ride => ride.RideProviderId == userId).ToList();

            return offeredRides;
        }

        public List<BookedRides> GetAllBookedRidesByUserId(int userId)
        {
            List<BookedRides> offeredRides = new List<BookedRides>();

            offeredRides = carPoolDBContext.BookedRides.Where(ride => ride.BookedUserId == userId).ToList();

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

        public List<Locations> GetLocations()
        {
            List<Locations> locations = carPoolDBContext.Locations.ToList();

            return locations;
        }

        public string GetLocationById(int id)
        {
            string name = carPoolDBContext.Locations.FirstOrDefault(location => location.LocationId == id).LocationName;
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
