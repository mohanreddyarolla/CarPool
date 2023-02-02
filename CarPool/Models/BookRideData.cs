namespace CarPool.Models
{
    public class BookRideData
    {
        public int UserId { get; set; }
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set;}
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

    }

}
