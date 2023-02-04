using CarPool.IServices;
using CarPool.Models;

namespace CarPool.Services
{
    public class MyRidesSupport:IMyRideSupport
    {
        IDataBaseService dataBaseService;
        public MyRidesSupport(IDataBaseService _dataBaseService)
        {
            dataBaseService = _dataBaseService;
        }
        public MyRides ProcessUserRides(int userId)
        {
            MyRides myRides = new MyRides();

            myRides.OfferedRides = dataBaseService.GetAllOfferedRidesByUserId(userId);

            myRides.BookedRides = dataBaseService.GetAllBookedRidesByUserId(userId);

            return myRides;

        }
    }
}
