namespace CarPool.Models
{
    public class SignUpRequest
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConformPassword { get; set; }
    }
}
