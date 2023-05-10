using Carpool.Models;

namespace CarPool.Interface
{
    public interface IMyRideSupport
    {
        public Task<MyRides> ProcessUserRides(int userId);
    }
}
