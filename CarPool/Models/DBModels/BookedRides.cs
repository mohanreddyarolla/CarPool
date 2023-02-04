using System.ComponentModel.DataAnnotations;

namespace CarPool.Models.DBModels
{
    public class BookedRides
    {
        [Key]
        public int BookedRideId { get; set; }
        public int BookedUserId { get; set; }
        public int StartPointId { get; set; }
        public int StopPointId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public int ReservedSeats { get; set; }
        public int RideProviderId { get; set; }


    }
}
