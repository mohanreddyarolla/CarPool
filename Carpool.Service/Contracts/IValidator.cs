using Carpool.Models;

namespace CarPool.Interface
{
    public interface IValidator
    {
        public Boolean Validate(SignUpRequest signUpData);
        public Task<int> ConfirmUserIdentity(LogInRequest logInData);
        public Boolean HasMatchingPickupAndDropoff(int startLocationId, int endLocationId, List<int> stopList);
        public Task<Boolean> HasRoomForPassengers(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);
        public Task<Boolean> IsUserNameExist(string newUserName);

    }
}

