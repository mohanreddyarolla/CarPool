using CarPool.Interface;
using CarPool.Models.DBModels;
using CarPool.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public HomeController(IDataBaseService _dataBaseService)
        {
            dataBaseService = _dataBaseService;
        }

        [HttpGet("GetAllLocations")]
        public ActionResult GetAllLocations()
        {
            return  Ok(JsonSerializer.Serialize(dataBaseService.GetLocations()));
        }

        [HttpGet("GetUserName/{userId}")]
        public ActionResult GetUserName(int userId)
        { 
            return  Ok(JsonSerializer.Serialize(dataBaseService.GetUserName(userId)));
        }

        [HttpGet("GetAvailableSeats/{AvailableRideId}/{LocationId}")]
        public ActionResult GetAvailableSeats(int AvailableRideId, int LocationId)
        {
            return Ok(dataBaseService.GetAvailableSeats(AvailableRideId, LocationId));
        }

        [HttpGet("GetOfferedRidesById/{RideId}")]
        public ActionResult GetOfferedRidesById(int RideId)
        {
            return Ok(JsonSerializer.Serialize(dataBaseService.GetAvailableRidesById(RideId)));
        }


    }
}
