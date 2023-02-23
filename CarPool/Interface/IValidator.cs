using CarPool.Models;

namespace CarPool.Interface
{
    public interface IValidator
    {
        public Boolean Validate(SignUpRequest signUpData);
        public int ConfirmUserIdentity(LogInRequest logInData);
        public Boolean HasMatchingPickupAndDropoff(int startLocationId, int endLocationId, List<int> stopList);
        public Boolean HasRoomForPassengers(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);
        public Boolean IsUserNameExist(string newUserName);

    }
}

