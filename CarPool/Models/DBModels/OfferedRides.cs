using System.ComponentModel.DataAnnotations;

namespace CarPool.Models.DBModels
{
    public class OfferedRides
    {
        [Key]
        public int OfferedRideId { get; set; }
        public int TotalPrice { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string StopList { get; set; }
        public int RideProviderId { get; set; }
        public string CurrentState { get; set; }
        public int SeatsProvided { get; set; }

    }
}
