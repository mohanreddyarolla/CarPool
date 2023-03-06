using CarPool.Interface;
using CarPool.Models;

namespace CarPool.Services
{
    public class LogInSupport:ILogInSupport
    {
        IValidator validation;
        IDataBaseService dataBaseService;
        IConfiguration configuration;
        public LogInSupport(IValidator _validation,IDataBaseService _dataBaseService,IConfiguration _configuration) 
        { 
            validation = _validation;
            dataBaseService = _dataBaseService;
            configuration = _configuration;
        }

        public Message ProcessLogIn(LogInRequest logInRequest)
        {
            Message message= new Message();

            int userId = validation.ConfirmUserIdentity(logInRequest);

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
