using CarPool.Models.DBModels;
using CarPool.Models;

namespace CarPool.IServices
{
    public interface IBookARideService
    {
        public List<AvailableRides> GetAvailableRidesToBook(BookRideData bookRideData);
    }
}
