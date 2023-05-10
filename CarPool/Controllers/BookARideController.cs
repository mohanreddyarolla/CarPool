using CarPool.Interface;
using Carpool.Models;
using Carpool.Models.DBModels;
using Carpool.Models.ViewModel;
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
        public async  Task<List<MatchingRide>> GetRideDetails(RideData bookRideData)
        {

            return await  bookARideService.GetAvailableRidesToBook(bookRideData);
        }

        [HttpGet("GetBookingCard/{AvailableRideId}/{FromLocationID}/{ToLocationID}")]
        public async Task<ActionResult< BookingCard>> GetBookingCard(int AvailableRideId, int FromLocationID, int ToLocationID)
        {
            return Ok(JsonSerializer.Serialize(await bookARideService.GetBookingCard(AvailableRideId, FromLocationID, ToLocationID)));
        }

        [HttpPost("Book")]
        public async Task<ActionResult> BookARideById(RideBookingRequest rideBookingData)
        {
            string status = await bookARideService.BookARide(rideBookingData);
            return Ok(JsonSerializer.Serialize(status));
        }
    }
}
