using CarPool.Models;

namespace CarPool.IServices
{
    public interface IValidation
    {
        public Boolean ValidateSignUpData(SignUpData signUpData);
        public Boolean ValidateUser(LogInData logInData);
    }
}
