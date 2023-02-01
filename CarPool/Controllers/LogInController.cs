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
        private CarPoolDBContext carPoolDBContext;
        ILogInService logInService;
        
        public LogInController(CarPoolDBContext _carPoolDBContext, ILogInService _logInServices)
        {
            carPoolDBContext = _carPoolDBContext;
            logInService = _logInServices;
           
        }

        

        [HttpPost]
        public ActionResult GetLogInData(LogInData logInData)
        {

            if (logInService.LogIn(logInData))
                return Ok("LogIn Successful");
            return BadRequest("LogIn UnSuccessful");
        }
    }
}
