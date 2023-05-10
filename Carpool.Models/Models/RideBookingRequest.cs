namespace Carpool.Models
{
    public class RideBookingRequest
    {
        public int UserId { get; set; }
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
        public int AvailableRideId { get; set; }
        public int RequiredSeats { get; set; }
    }
}
