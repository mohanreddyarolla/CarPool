using CarPool.Models;

namespace CarPool.IServices
{
    public interface ISignUpSupport
    {
        public string ProcessSignUp(SignUpRequest signUpData);

    }
}
