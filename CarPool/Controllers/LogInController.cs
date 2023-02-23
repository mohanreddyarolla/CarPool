using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
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
        public ActionResult GetLogInData(LogInRequest logInData)
        {
            Message status = logInService.ProcessLogIn(logInData);

            return Ok(JsonSerializer.Serialize(status));
            
        }
    }
}
