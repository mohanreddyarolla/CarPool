using CarPool.Interface.IRepository;
using Carpool.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Repository
{
    public class OfferedRidesRepository:IOfferedRidesRepository
    {
        CarPoolDBContext carPoolDBContext;

        public OfferedRidesRepository(CarPoolDBContext _carPoolDBContext)
        {
            carPoolDBContext = _carPoolDBContext;

        }


        public async Task<int> SaveRideOffer(OfferedRide newRide)
        {

            try
            {
                await carPoolDBContext.OfferedRide.AddAsync(newRide);
                await carPoolDBContext.SaveChangesAsync();
                return newRide.OfferedRideId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return -1;
            }

        }

        public async Task MakeRideInActive(OfferedRide newRide)
        {
            try
            {
                newRide.CurrentState = "InActive";
                carPoolDBContext.OfferedRide.Update(newRide);
                await carPoolDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //should revoke the available ride inservsion or just use status false
                Console.WriteLine($"{ex.Message}");
            }
        }

        public async Task<List<OfferedRide>> GetAvailbleRides()
        {
            List<OfferedRide> matchingRides = new List<OfferedRide>();

            try
            {
                matchingRides = await carPoolDBContext.OfferedRide.Where(ride => ride.CurrentState == "Active").ToListAsync();
               /* foreach (OfferedRide ride in carPoolDBContext.OfferedRide)
                {
                    if (ride.CurrentState == "Active")
                    {
                        matchingRides.Add(ride);
                        Console.WriteLine(ride.RideProviderId);
                    }

                }*/
            }
            catch (Exception ex)
            {

            }


            return matchingRides;
        }


        public async Task<OfferedRide> GetAvailableRidesById(int id)
        {
            OfferedRide offeredRides = new OfferedRide();

            try
            {
                offeredRides = await carPoolDBContext.OfferedRide.FirstOrDefaultAsync(ride => ride.OfferedRideId == id);

            }
            catch (Exception ex) { }


            return offeredRides;
        }


        public async Task<List<OfferedRide>> GetAllOfferedRidesByUserId(int userId)
        {
            List<OfferedRide> offeredRides = new List<OfferedRide>();

            offeredRides = await carPoolDBContext.OfferedRide.Where(ride => ride.RideProviderId == userId).ToListAsync();

            return offeredRides;
        }





    }
}
