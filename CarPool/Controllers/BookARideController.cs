using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
using CarPool.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarPool.Controllers
{
    [Authorize]
    [Route("api/CarPool/[controller]")]
    [ApiController]
    
    public class BookARideController : ControllerBase
    {

        IBookARideService bookARideService;
        public BookARideController(IBookARideService _bookARideService)
        {
            bookARideService= _bookARideService;
        }

        [HttpPost("GetAvailableRides")]
        public ActionResult<List<MatchingRide>> GetRideDetails(RideData bookRideData)
        {

            return  Ok(JsonSerializer.Serialize( bookARideService.GetAvailableRidesToBook(bookRideData).ToList()));
        }

        [HttpGet("GetBookingCard/{AvailableRideId}/{FromLocationID}/{ToLocationID}")]
        public ActionResult GetBookingCard(int AvailableRideId, int FromLocationID, int ToLocationID)
        {
            return Ok(JsonSerializer.Serialize(bookARideService.GetBookingCard(AvailableRideId, FromLocationID, ToLocationID)));
        }

        [HttpPost("Book")]
        public ActionResult BookARideById(RideBookingRequest rideBookingData)
        {

            string status = bookARideService.BookARide(rideBookingData);
            return Ok(JsonSerializer.Serialize(status));
        }
    }
}
