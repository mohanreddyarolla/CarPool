using CarPool.IServices;
using CarPool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class MyRidesController : ControllerBase
    {
        IMyRideSupport myRideSupport;
        public MyRidesController(IMyRideSupport _myRideSupport)
        {
            myRideSupport = _myRideSupport;
        }

        [HttpGet("{userId}")]
        public ActionResult<MyRides> GetMyRides(int userId)
        {
            Console.WriteLine(".......");
            return myRideSupport.ProcessUserRides(userId);
        }
    }
}
