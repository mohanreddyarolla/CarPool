
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carpool.Models.DBModels
{

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; } 


    }
}
