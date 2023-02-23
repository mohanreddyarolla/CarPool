using CarPool.Interface;
using CarPool.Models;

namespace CarPool.Services
{
    public class LogInSupport:ILogInSupport
    {
        IValidator validation;
        IDataBaseService dataBaseService;
        public LogInSupport(IValidator _validation,IDataBaseService _dataBaseService) 
        { 
            validation = _validation;
            dataBaseService = _dataBaseService;
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
