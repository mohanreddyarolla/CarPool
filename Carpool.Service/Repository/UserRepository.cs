using CarPool.Interface.IRepository;
using Carpool.Models;
using Carpool.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Repository
{
    public class UserRepository:IUserRepository
    {
        CarPoolDBContext carPoolDBContext;

        public UserRepository(CarPoolDBContext _carPoolDBContext)
        {
            carPoolDBContext = _carPoolDBContext;

        }

        public async Task<int> SaveUserSignUpDetails(User newUser)
        {
            try
            {
                await carPoolDBContext.Users.AddAsync(newUser);
                await carPoolDBContext.SaveChangesAsync();
                return newUser.UserId;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public async Task<User> FetchUserData(LogInRequest logInData)
        {
            var userData = await carPoolDBContext.Users.FirstOrDefaultAsync(user => user.EmailId == logInData.EmailId && user.Password == logInData.Password);

            return userData;
        }

        public async Task<List<String>> GetAllUserNames()
        {
            List<string> userNames = await carPoolDBContext.Users.Select(user => user.EmailId).ToListAsync();

            /*foreach (string userName in userNames)
            {
                yield return userName;
            }*/
            return userNames;
        }

        public async Task<int> GetUserId(string EmailId)
        {
            User user = await carPoolDBContext.Users.FirstOrDefaultAsync(user => user.EmailId == EmailId);

            return user.UserId;
        }

        public async Task<string> GetUserName(int userId)
        {
            var user = await carPoolDBContext.Users.FirstOrDefaultAsync(user =>
            user.UserId == userId);

            return user.Name;
        }

        public async Task<User> GetUserData(int userId)
        {
            try
            {
                User user = await carPoolDBContext.Users.FirstOrDefaultAsync(user => user.UserId == userId);

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> UpdateUserData(User user)
        {
            try
            {
                carPoolDBContext.Users.Update(user);
                await carPoolDBContext.SaveChangesAsync();

                return "Updated Succefully!";
            }
            catch (Exception e)
            {
                return "Update UNSuccessful";
            }
        }

    }
}
