using CarPool.IServices;
using CarPool.Models;

namespace CarPool.Services
{
    public class Validation:IValidation
    {
        IDataBaseService dataBaseService;
        
        public Validation(IDataBaseService _dataBaseService)
        {
            dataBaseService= _dataBaseService;
            
        }
        public Boolean ValidateSignUpData(SignUpData signUpData) 
        {
            if(signUpData.Name == null || signUpData.EmailId == null || signUpData.Password != signUpData.ConformPassword)
            {
                return false;
            }

            return true;
          
        }

        public Boolean ValidateUser(LogInData logInData)
        {
            var user = dataBaseService.GetUser(logInData);

            if(user != null)
            {
                return true;
            }
            return false;
        }

    }
}
