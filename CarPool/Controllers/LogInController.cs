using CarPool.Interface;
using Carpool.Models;
using Carpool.Models.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
 
        ILogInSupport logInService;
        
        public LogInController(ILogInSupport _logInServices)
        {
            logInService = _logInServices;
           
        }

        [HttpPost]
        public async Task<ActionResult> GetLogInData(LogInRequest logInData)
        {
            Message status = await logInService.ProcessLogIn(logInData);

            return Ok(JsonSerializer.Serialize(status));
            
        }
    }
}
