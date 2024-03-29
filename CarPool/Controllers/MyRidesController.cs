﻿using CarPool.Interface;
using Carpool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    [Authorize]
    public class MyRidesController : ControllerBase
    {
        IMyRideSupport myRideSupport;
        public MyRidesController(IMyRideSupport _myRideSupport)
        {
            myRideSupport = _myRideSupport;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<MyRides>> GetMyRides(int userId)
        {
            
            return  Ok(JsonSerializer.Serialize(await myRideSupport.ProcessUserRides(userId)));
        }
    }
}
