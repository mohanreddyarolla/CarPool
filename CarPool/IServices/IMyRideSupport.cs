using CarPool.Models;

namespace CarPool.IServices
{
    public interface IMyRideSupport
    {
        public MyRides ProcessUserRides(int userId);
    }
}
