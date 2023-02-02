using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.Services
{
    public class SignUpService:ISignUpService
    {
        IValidation validation;
        IDataBaseService dataBaseService;
        public SignUpService(IValidation _validation,IDataBaseService _dataBaseService) 
        { 
            validation = _validation;
            dataBaseService= _dataBaseService;

        }
        public bool SaveSignUpData(SignUpData signUpData)
        {
            if(validation.ValidateSignUpData(signUpData))
            {
                User newUser = new User();
                newUser.Name = signUpData.Name;
                newUser.Password = signUpData.Password;
                newUser.EmailId = signUpData.EmailId;

                if (dataBaseService.SaveSignUpDataInUsers(newUser))
                    return true;
                else
                    return false;

            }
            return false;
        }
    }
}
