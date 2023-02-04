using CarPool.Models.DBModels;

namespace CarPool.Models
{
    public class MyRides
    {
        public List<OfferedRides> OfferedRides { set; get; }
        public List<BookedRides> BookedRides { set; get; }

    }
}
