using CarPool.Models;

namespace CarPool.IServices
{
    public interface IValidation
    {
        public Boolean ValidateSignUpData(SignUpData signUpData);
        public Boolean ValidateUser(LogInData logInData);
        public Boolean CheckForSourceDestinationMatch(int startLocationId, int endLocationId, List<int> stopList);
    }
}
