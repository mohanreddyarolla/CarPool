using CarPool.Models;

namespace CarPool.IServices
{
    public interface ISignUpService
    {
        public bool SaveSignUpData(SignUpData signUpData);

    }
}
