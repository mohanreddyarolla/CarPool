using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
using CarPool.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private CarPoolDBContext carPoolDBContext;
        ISignUpSupport signUpSupport;
        public SignUpController(CarPoolDBContext _carPoolDBContext, ISignUpSupport _signUpSupport)
        {
            carPoolDBContext = _carPoolDBContext;
            signUpSupport = _signUpSupport;
        }

        [HttpGet]
        public IEnumerable<User> GetUsersList()
        {
            return carPoolDBContext.Users;
        }

        [HttpPost]
        public ActionResult GetSignUpDetails(SignUpRequest signUpData)
        {
            Console.WriteLine("Hi");
            Message status = signUpSupport.ProcessSignUp(signUpData);
           
            return Ok(JsonSerializer.Serialize(status));
            
        }
       
    }
}

