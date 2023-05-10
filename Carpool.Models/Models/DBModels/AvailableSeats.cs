using System.ComponentModel.DataAnnotations;

namespace Carpool.Models.DBModels
{
    public class AvailableSeats
    {
        [Key]
        public int Id { get; set; }
        public int AvailableRideId { get; set; }
        public int LocationId { get; set; }
        public int SeatAvailability { get; set; }

    }
}
