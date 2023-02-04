using CarPool.Models;

namespace CarPool.IServices
{
    public interface ILogInSupport
    {
        public string ProcessLogIn(LogInData logInData);
    }
}
