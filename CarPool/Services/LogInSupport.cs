using CarPool.IServices;
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

        public string ProcessLogIn(LogInRequest logInRequest)
        {
            
            if(validation.ConfirmUserIdentity(logInRequest))
            {
                return "Login successful! , Enjoy the ride with us";
            }
                
            return "Login failed! Please check your username and password and try again.";
        }
    }
}
