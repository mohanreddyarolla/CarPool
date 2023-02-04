using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;
using CarPool.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private CarPoolDBContext carPoolDBContext;
        ISignUpSupport signUpService;
        public SignUpController(CarPoolDBContext _carPoolDBContext, ISignUpSupport _signUpService)
        {
            carPoolDBContext = _carPoolDBContext;
            signUpService = _signUpService;
        }

        [HttpGet]
        public IEnumerable<User> GetUsersList()
        {
            return carPoolDBContext.Users;
        }

        [HttpPost]
        public ActionResult GetSignUpDetails(SignUpData signUpData)
        {
            string status = signUpService.ProcessSignUp(signUpData);
           
            return Ok(status);
            
        }
       
    }
}

