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
        ISignUpService signUpService;
        public SignUpController(CarPoolDBContext _carPoolDBContext, ISignUpService _signUpService)
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
            Console.WriteLine("InSignUpController");
            if (signUpService.SaveSignUpData(signUpData))
                return Ok("Sign in Successful");
            else
                return BadRequest("SignIn UnSuccessful");
        }
       
    }
}

