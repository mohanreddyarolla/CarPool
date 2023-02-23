using CarPool.Models;

namespace CarPool.Interface
{
    public interface IMyRideSupport
    {
        public MyRides ProcessUserRides(int userId);
    }
}
