using Carpool.Models;

namespace CarPool.Interface
{
    public interface ILogInSupport
    {
        public Task<Message> ProcessLogIn(LogInRequest logInData);
    }
}
