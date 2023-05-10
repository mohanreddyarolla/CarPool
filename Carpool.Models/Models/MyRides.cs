using Carpool.Models.DBModels;

namespace Carpool.Models
{
    public class MyRides
    {
        public List<OfferedRide> OfferedRides { set; get; }
        public List<BookedRide> BookedRides { set; get; }

    }
}
