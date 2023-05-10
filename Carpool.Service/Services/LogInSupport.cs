using CarPool.Interface;
using Carpool.Models;
using Microsoft.Extensions.Configuration;

namespace CarPool.Services
{
    public class LogInSupport:ILogInSupport
    {
        IValidator validation;
        IConfiguration configuration;
        public LogInSupport(IValidator _validation,IConfiguration _configuration) 
        { 
            validation = _validation;
            
            configuration = _configuration;
        }

        public async Task<Message> ProcessLogIn(LogInRequest logInRequest)
        {
            Message message= new Message();

            int userId =  await validation.ConfirmUserIdentity(logInRequest);

            if(userId != -1)
            {
                message.UserId = userId;
                message.Status = true;
                message.StatusMessage = "Login successful! , Enjoy the ride with us";
                message.Token = new TokenGenerator(configuration).GenerateToken(userId.ToString());
                return message;
            }
            else
            {
                message.UserId = userId;
                message.Status = false;
                message.StatusMessage = "Login failed! Please check your username and password and try again.";
                return message;
            }

           

        }
    }
}
