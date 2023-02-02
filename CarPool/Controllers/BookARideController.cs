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

        [HttpPost]
        public IEnumerable<AvailableRides> GetRideDetails(BookRideData bookRideData)
        {
            return bookARideService.GetAvailableRidesToBook(bookRideData).ToList();
        }
    }
}
