using CarPool.Interface;
using CarPool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            
            return Ok(JsonSerializer.Serialize(myRideSupport.ProcessUserRides(userId)));
        }
    }
}
