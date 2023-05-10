using CarPool.Interface;
using CarPool.Interface.IRepository;
using Carpool.Models;

namespace CarPool.Services
{
    public class MyRidesSupport:IMyRideSupport
    {
      
        IOfferedRidesRepository offeredRidesRepository;
        IBookedRidesRepository bookedRidesRepository;
        public MyRidesSupport(IBookedRidesRepository _bookedRidesRepository,IOfferedRidesRepository _offeredRidesRepository)
        {
            
            bookedRidesRepository = _bookedRidesRepository;
            offeredRidesRepository = _offeredRidesRepository; 
        }
        public async Task<MyRides> ProcessUserRides(int userId)
        {
            MyRides myRides = new MyRides();

            myRides.OfferedRides = await offeredRidesRepository.GetAllOfferedRidesByUserId(userId);

            myRides.BookedRides = await bookedRidesRepository.GetAllBookedRidesByUserId(userId);

            return myRides;

        }
    }
}

