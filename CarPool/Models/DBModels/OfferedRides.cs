using System.ComponentModel.DataAnnotations;

namespace CarPool.Models.DBModels
{
    public class OfferedRides
    {
        [Key]
        public int OfferedRideId { get; set; }
        public int TotalPrice { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }
        public string StopList { get; set; }
        public int RideProviderId { get; set; }
        public string CurrentState { get; set; }

    }
}
