using Carpool.Models;

namespace CarPool.Interface
{
    public interface ISignUpSupport
    {
        public Task<Message> ProcessSignUp(SignUpRequest signUpData);

    }
}
