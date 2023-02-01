
using System.ComponentModel.DataAnnotations;

namespace CarPool.Models.DBModels
{
    public class Locations
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
