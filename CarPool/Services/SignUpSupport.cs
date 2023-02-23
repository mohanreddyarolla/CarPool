using CarPool.Interface;
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
        public Message ProcessSignUp(SignUpRequest signUpRequest)
        {
            Message message = new Message();
            if (validation.IsUserNameExist(signUpRequest.EmailId))
            {
                message.Status = false;
                message.StatusMessage = "Sorry, The Email Id is allready taken, Please provide another Email Id." ;
                return message;
            }
            if(validation.Validate(signUpRequest) )
            {
                User newUser = new User();
                newUser.Name = signUpRequest.Name;
                newUser.Password = signUpRequest.Password;
                newUser.EmailId = signUpRequest.EmailId;

                int userId = dataBaseService.SaveUserSignUpDetails(newUser);
                message.UserId = userId;

                if (userId != -1)
                {
                    
                     message.Status = true;
                     message.StatusMessage = "Congratulations, your account has been created.";
                     return message;
                }
                else
                {
                    
                    message.Status = false;
                    message.StatusMessage = "Sorry, there was a problem with our database and we were unable to process your sign-up. Please try again later.";
                    return message;
                }
                    

            }
            message.Status = false;
            message.StatusMessage = "Your sign-up was unsuccessful due to some invalid information. Please check and try again.";
            return message;
        }
    }
}
