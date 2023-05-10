using Carpool.Models;
using Carpool.Models.DBModels;

namespace CarPool.Interface.IRepository
{
    public interface IUserRepository
    {
        public  Task<int> SaveUserSignUpDetails(User newUser);
        public Task<User> FetchUserData(LogInRequest logInData);
        public Task<List<String>> GetAllUserNames();
        public Task<int> GetUserId(string EmailId);
        public Task<string> GetUserName(int userId);
        public Task<User> GetUserData(int userId);
        public Task<string> UpdateUserData(User user);
    }
}
