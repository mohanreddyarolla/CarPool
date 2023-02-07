using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.Services
{
    public class SignUpSupport:ISignUpSupport
    {
        IValidator validation;
        IDataBaseService dataBaseService;
        public SignUpSupport(IValidator _validation,IDataBaseService _dataBaseService) 
        { 
            validation = _validation;
            dataBaseService= _dataBaseService;

        }
        public string ProcessSignUp(SignUpRequest signUpRequest)
        {
            if(validation.IsUserNameExist(signUpRequest.EmailId))
            {
                return "Sorry, The Email Id is allready taken, Please provide another Email Id.";
            }
            if(validation.Validate(signUpRequest) )
            {
                User newUser = new User();
                newUser.Name = signUpRequest.Name;
                newUser.Password = signUpRequest.Password;
                newUser.EmailId = signUpRequest.EmailId;

                if (dataBaseService.SaveUserSignUpDetails(newUser))
                    return "Congratulations, your account has been created.";
                else
                    return "Sorry, there was a problem with our database and we were unable to process your sign-up. Please try again later.";

            }
            return "Your sign-up was unsuccessful due to some invalid information. Please check and try again.";
        }
    }
}
