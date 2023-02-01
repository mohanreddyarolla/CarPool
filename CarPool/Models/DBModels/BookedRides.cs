using System.ComponentModel.DataAnnotations;

namespace CarPool.Models.DBModels
{
    public class BookedRides
    {
        [Key]
        public int BookedRideId { get; set; }
        public int UserId { get; set; }
        public int StartPointId { get; set; }
        public int EndPointId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }

    }
}
