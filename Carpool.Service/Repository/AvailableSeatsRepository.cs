using CarPool.Interface.IRepository;
using Carpool.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Repository
{
    public class AvailableSeatsRepository:IAvailableSeatsRepository
    {
        CarPoolDBContext carPoolDBContext;

        public AvailableSeatsRepository(CarPoolDBContext _carPoolDBContext)
        {
            carPoolDBContext = _carPoolDBContext;

        }

        public async Task<bool> SaveInAvailableSeats(AvailableSeats newSeats)
        {
            try
            {
                await carPoolDBContext.AvailableSeats.AddAsync(newSeats);
                await carPoolDBContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                //should revoke the available ride inservsion or just use status false
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public async Task<List<int>> GetAvailableSeatsList(int availableRideId, List<int> stopListIds)
        {
            List<int> availableSeats = new List<int>();

            try
            {
                foreach (int id in stopListIds)
                {
                    var seats = await carPoolDBContext.AvailableSeats.FirstOrDefaultAsync(seat => seat.AvailableRideId == availableRideId && seat.LocationId == id);
                    if (seats != null)
                        availableSeats.Add(seats.SeatAvailability);
                }
            }
            catch (Exception ex)
            {

            }



            return availableSeats;
        }


        public async Task<Boolean> ReserveSeats(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId)
        {
            try
            {
                int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
                int ToLocationIndex = stopListIds.IndexOf(ToLocationId);

                for (int i = FromLocationIndex; i < ToLocationIndex; i++)
                {
                    AvailableSeats seats = await carPoolDBContext.AvailableSeats.FirstOrDefaultAsync(seats => seats.AvailableRideId == rideId && seats.LocationId == stopListIds[i]);

                    if (seats != null)
                    {
                        seats.SeatAvailability -= requiredSeats;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                await carPoolDBContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<int> GetAvailableSeats(int AvailableRideId, int LocationId)
        {
            var seats = await carPoolDBContext.AvailableSeats.FirstOrDefaultAsync(seat => seat.LocationId == LocationId && seat.AvailableRideId == AvailableRideId);
            return seats.SeatAvailability;
        }


    }
}
