using CarPool.IServices;
using CarPool.Models;

namespace CarPool.Services
{
    public class LogInSupport:ILogInSupport
    {
        IValidation validation;
        public LogInSupport(IValidation _validation) 
        { 
            validation = _validation;
        }

        public string ProcessLogIn(LogInData logInData)
        {
            if(validation.ConfirmUserIdentity(logInData))
                return "Login successful! Enjoy the ride with us";
            return "Login failed! Please check your username and password and try again.";
        }
    }
}
