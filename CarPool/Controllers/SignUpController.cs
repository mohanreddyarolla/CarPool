using CarPool.Interface;
using Carpool.Models;
using Carpool.Models.DBModels;
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
        public async Task<ActionResult> GetSignUpDetails(SignUpRequest signUpData)
        {
            
            Message status = await signUpSupport.ProcessSignUp(signUpData);
           
            return Ok(JsonSerializer.Serialize(status));
            
        }
       
    }
}

