using CarPool.Models;

namespace CarPool.IServices
{
    public interface IValidator
    {
        public Boolean Validate(SignUpRequest signUpData);
        public Boolean ConfirmUserIdentity(LogInRequest logInData);
        public Boolean HasMatchingPickupAndDropoff(int startLocationId, int endLocationId, List<int> stopList);
        public Boolean HasRoomForPassengers(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);
        public Boolean IsUserNameExist(string newUserName);

    }
}

