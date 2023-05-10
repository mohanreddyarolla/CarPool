using System.ComponentModel.DataAnnotations;

namespace Carpool.Models.DBModels
{
    public class BookedRide
    {
        [Key]
        public int BookedRideId { get; set; }
        public int BookedUserId { get; set; }
        public int StartPointId { get; set; }
        public int StopPointId { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }
        public int ReservedSeats { get; set; }
        public int RideProviderId { get; set; }


    }
}
