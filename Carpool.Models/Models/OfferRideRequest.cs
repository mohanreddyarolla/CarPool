using System.ComponentModel.DataAnnotations;

namespace Carpool.Models
{
    public class OfferRideRequest
    {
        public int TotalPrice { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string StopList { get; set; }
        public int RideProviderId { get; set;}
        public int TotalSeats { get; set; }
        public string CurrentState { get; set; }
    }
}
