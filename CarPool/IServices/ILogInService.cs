using CarPool.Models;

namespace CarPool.IServices
{
    public interface ILogInService
    {
        public Boolean LogIn(LogInData logInData);
    }
}
