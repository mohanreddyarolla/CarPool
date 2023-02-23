using CarPool.Models;

namespace CarPool.Interface
{
    public interface ILogInSupport
    {
        public Message ProcessLogIn(LogInRequest logInData);
    }
}
