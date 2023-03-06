using CarPool.Interface;
using CarPool.Models.DBModels;
using CarPool.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet("GetAvailableSeats/{AvailableRideId}/{LocationId}")]
        public ActionResult GetAvailableSeats(int AvailableRideId, int LocationId)
        {
            return Ok(dataBaseService.GetAvailableSeats(AvailableRideId, LocationId));
        }
        [Authorize]
        [HttpGet("GetOfferedRidesById/{RideId}")]
        public ActionResult GetOfferedRidesById(int RideId)
        {
            return Ok(JsonSerializer.Serialize(dataBaseService.GetAvailableRidesById(RideId)));
        }

        [Authorize]
        [HttpGet("GetUserData/{userId}")]
        public ActionResult GetUserData(int userId)
        {
            return Ok(JsonSerializer.Serialize(dataBaseService.GetUserData(userId)));
        }

        [Authorize]
        [HttpPost("UpdateUserData")]
        public ActionResult UpdateUserData(User user)
        {
            return Ok(JsonSerializer.Serialize(dataBaseService.UpdateUserData(user)));
        }


    }
}
