using CarPool.Models;

namespace CarPool.Interface
{
    public interface ISignUpSupport
    {
        public Message ProcessSignUp(SignUpRequest signUpData);

    }
}
