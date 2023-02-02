using System.ComponentModel.DataAnnotations;

namespace CarPool.Models.DBModels
{
    public class AvailableRides
    {
        [Key]
        public int AvailableRideId { get; set; }
        public int TotalPrice { get; set; }

        
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        
        public DateTime Date { get; set; }

        public string StopList { get; set; }
        public int UserId { get; set; }

        public string CurrentState { get; set; }

    }
}
