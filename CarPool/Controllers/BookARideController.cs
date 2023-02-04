using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class BookARideController : ControllerBase
    {
        IBookARideService bookARideService;
        public BookARideController(IBookARideService _bookARideService)
        {
            bookARideService= _bookARideService;
        }

        [HttpPost("RideData")]
        public IEnumerable<OfferedRides> GetRideDetails(RideData bookRideData)
        {
            return bookARideService.GetAvailableRidesToBook(bookRideData).ToList();
        }

        [HttpPost("Book")]
        public ActionResult BookARideById(RideBookingData rideBookingData)
        {
            Console.WriteLine("mkkk");
            string status = bookARideService.BookARide(rideBookingData);
            
            return Ok(status);
        }
    }
}
