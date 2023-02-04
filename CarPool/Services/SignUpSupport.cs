using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.Services
{
    public class SignUpSupport:ISignUpSupport
    {
        IValidation validation;
        IDataBaseService dataBaseService;
        public SignUpSupport(IValidation _validation,IDataBaseService _dataBaseService) 
        { 
            validation = _validation;
            dataBaseService= _dataBaseService;

        }
        public string ProcessSignUp(SignUpData signUpData)
        {
            if(validation.ValidateNewUserRegistration(signUpData))
            {
                User newUser = new User();
                newUser.Name = signUpData.Name;
                newUser.Password = signUpData.Password;
                newUser.EmailId = signUpData.EmailId;

                if (dataBaseService.SaveUserSignUpDetails(newUser))
                    return "Congratulations, your account has been created.";
                else
                    return "Sorry, there was a problem with our database and we were unable to process your sign-up. Please try again later.";

            }
            return "Your sign-up was unsuccessful due to some invalid information. Please check and try again.";
        }
    }
}
