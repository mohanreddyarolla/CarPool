using CarPool.Models;

namespace CarPool.IServices
{
    public interface IValidation
    {
        public Boolean ValidateNewUserRegistration(SignUpData signUpData);
        public Boolean ConfirmUserIdentity(LogInData logInData);
        public Boolean HasMatchingPickupAndDropoff(int startLocationId, int endLocationId, List<int> stopList);
        public Boolean HasRoomForPassengers(List<int> stopListIds, int rideId, int requiredSeats, int fromLocationId, int ToLocationId);
    }
}
