namespace CarPool.Models.ViewModel
{
    public class MatchingRide
    {
        public int RideID { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Price { get; set; }
        public int SeatAvailability { get; set; }
    }
}
