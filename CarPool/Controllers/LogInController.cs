using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            string status = logInService.ProcessLogIn(logInData);

            return Ok(status);
            
        }
    }
}
