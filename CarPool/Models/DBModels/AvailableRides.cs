using System.ComponentModel.DataAnnotations;

namespace CarPool.Models.DBModels
{
    public class AvailableRides
    {
        [Key]
        public int AvailableRideId { get; set; }
        public int TotalPrice { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; }
        public string StopList { get; set; }
        public int UserId { get; set; }

    }
}
