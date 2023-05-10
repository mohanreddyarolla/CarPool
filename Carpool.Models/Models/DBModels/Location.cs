
using System.ComponentModel.DataAnnotations;

namespace Carpool.Models.DBModels
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
