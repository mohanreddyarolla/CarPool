using CarPool.Interface;
using CarPool.Interface.IRepository;
using Carpool.Models;
using Carpool.Models.DBModels;
using CarPool.Repository;

namespace CarPool.Services
{
    public class SignUpSupport:ISignUpSupport
    {
        IValidator validation;
        IUserRepository userRepository;
        public SignUpSupport(IValidator _validation ,IUserRepository _userRepository) 
        { 
            validation = _validation;
            userRepository= _userRepository;

        }
        public async Task<Message> ProcessSignUp(SignUpRequest signUpRequest)
        {
            Message message = new Message();
            if (await validation.IsUserNameExist(signUpRequest.EmailId))
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

                int userId = await userRepository.SaveUserSignUpDetails(newUser);
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
