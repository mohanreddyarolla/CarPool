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

        public Boolean SaveUserSignUpDetails(User newUser)
        {
            
            carPoolDBContext.Users.Add(newUser);
            carPoolDBContext.SaveChanges();
            return true;
        }

        public User FetchUserData(LogInData logInData)
        {
            var userData = carPoolDBContext.Users.FirstOrDefault(user => user.EmailId == logInData.EmailId && user.Password == logInData.Password);

            return userData;
        }

        public int SaveRideOffer(OfferedRides newRide)
        {
           
            try
            {
                carPoolDBContext.AvailableRides.Add(newRide);
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
                carPoolDBContext.AvailableRides.Update(newRide);
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

            foreach(OfferedRides ride in  carPoolDBContext.AvailableRides)
            {
                if(ride.CurrentState == "Active")
                {
                    matchingRides.Add(ride);
                    Console.WriteLine(ride.RideProviderId);
                }
                    
            }


            return matchingRides;
        }
        
        public List<int> GetAvailableSeatsList(int availableRideId,List<int> stopListIds)
        {
            List<int> availableSeats = new List<int>();

            foreach (int id in stopListIds)
            {
                var seats = carPoolDBContext.AvailableSeats.FirstOrDefault(seat => seat.AvailableRideId == availableRideId && seat.LocationId == id);
                if (seats != null)
                    availableSeats.Add(seats.SeatAvailability);
            }           
            
            return availableSeats;
        }

        public OfferedRides GetAvailableRidesById(int id)
        {
            
             return carPoolDBContext.AvailableRides.FirstOrDefault(ride => ride.OfferedRideId == id) ;
            
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
                newRide.StartTime = ride.StartTime;
                newRide.EndTime = ride.EndTime;
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

            offeredRides = carPoolDBContext.AvailableRides.Where(ride => ride.RideProviderId == userId).ToList();

            return offeredRides;
        }

        public List<BookedRides> GetAllBookedRidesByUserId(int userId)
        {
            List<BookedRides> offeredRides = new List<BookedRides>();

            offeredRides = carPoolDBContext.BookedRides.Where(ride => ride.BookedUserId == userId).ToList();

            return offeredRides;
        }
    }
}
