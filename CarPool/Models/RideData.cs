namespace CarPool.Models
{
    public class RideData
    {
        
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set;}
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

    }

}
