using CarPool.IServices;
using CarPool.Models;

namespace CarPool.Services
{
    public class LogInService:ILogInService
    {
        IValidation validation;
        public LogInService(IValidation _validation) 
        { 
            validation = _validation;
        }

        public Boolean LogIn(LogInData logInData)
        {
            if(validation.ValidateUser(logInData))
                return true;
            return false;
        }
    }
}
