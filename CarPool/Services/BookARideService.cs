using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CarPool.Services
{
    public class BookARideService:IBookARideService
    {
        IDataBaseService dataBaseService;
        IValidation validation;

        public BookARideService(IDataBaseService _dataBaseService,IValidation _validation)
        {
            this.dataBaseService = _dataBaseService;
            validation = _validation;
        }

        public List<AvailableRides> GetAvailableRidesToBook(BookRideData bookRideData)
        {

            List<AvailableRides> rides = dataBaseService.GetAvailbleRides();

            List<AvailableRides> matchingRides = new List<AvailableRides>();

            foreach (AvailableRides ride in rides)
            {

                List<int> stopListIds = new List<int>(Array.ConvertAll(ride.StopList.Split(','), int.Parse));

                if (validation.CheckForSourceDestinationMatch(bookRideData.FromLocationId, bookRideData.ToLocationId, stopListIds) && ride.Date == bookRideData.Date)
                {
                    matchingRides.Add(ride);
                }
            }
            return matchingRides;
        }
    }
}
