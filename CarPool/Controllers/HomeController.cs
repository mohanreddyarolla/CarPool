using CarPool.Interface;
using Carpool.Models.DBModels;
using CarPool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CarPool.Interface.IRepository;

namespace CarPool.Controllers
{
    
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IOfferedRidesRepository offeredRidesRepository;
        IUserRepository userRepository;
        ILocationsRepository locationsRepository;
        IAvailableSeatsRepository availableSeatsRepository;
        public HomeController(IAvailableSeatsRepository availableSeatsRepository,IOfferedRidesRepository offeredRidesRepository,IUserRepository userRepository, ILocationsRepository locationsRepository)
        {
            this.availableSeatsRepository = availableSeatsRepository;
            this.offeredRidesRepository = offeredRidesRepository;
            this.userRepository = userRepository;
            this.locationsRepository = locationsRepository;
        }

        [HttpGet("GetAllLocations")]
        public async Task<ActionResult> GetAllLocations()
        {
            return  Ok(JsonSerializer.Serialize(await locationsRepository.GetLocations()));
        }
        
        [HttpGet("GetUserName/{userId}")]
        public async Task<ActionResult>  GetUserName(int userId)
        { 
            return  Ok(JsonSerializer.Serialize(await userRepository.GetUserName(userId)));
        }
        [Authorize]
        [HttpGet("GetAvailableSeats/{AvailableRideId}/{LocationId}")]
        public async Task<ActionResult> GetAvailableSeats(int AvailableRideId, int LocationId)
        {
            return Ok(await availableSeatsRepository.GetAvailableSeats(AvailableRideId, LocationId));
        }
        [Authorize]
        [HttpGet("GetOfferedRidesById/{RideId}")]
        public async Task<ActionResult> GetOfferedRidesById(int RideId)
        {
            return Ok(JsonSerializer.Serialize(await offeredRidesRepository.GetAvailableRidesById(RideId)));
        }

        [Authorize]
        [HttpGet("GetUserData/{userId}")]
        public async Task<ActionResult> GetUserData(int userId)
        {
            return Ok(JsonSerializer.Serialize(await userRepository.GetUserData(userId)));
        }

        [Authorize]
        [HttpPost("UpdateUserData")]
        public async Task<ActionResult> UpdateUserData(User user)
        {
            return Ok(JsonSerializer.Serialize(await userRepository.UpdateUserData(user)));
        }


    }
}
