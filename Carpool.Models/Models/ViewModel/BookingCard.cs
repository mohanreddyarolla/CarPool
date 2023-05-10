namespace Carpool.Models.ViewModel
{
    public class BookingCard
    {
        public string From { get; set; }
        public string To { get; set; }
        public int RequiredSeats { get; set; }
        public int AvailableSeats { get; set; }
        public List<string> StopList { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
    }
}
