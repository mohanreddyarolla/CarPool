using CarPool.Interface;
using CarPool.Interface.IRepository;
using Carpool.Models;

namespace CarPool.Services
{
    public class Validator : IValidator
    {
        IUserRepository userRepository;
        IAvailableSeatsRepository availableSeatsRepository;

        public Validator(IUserRepository _userRepository, IAvailableSeatsRepository _availableSeatsRepository)
        {
            userRepository = _userRepository;
            availableSeatsRepository= _availableSeatsRepository;


           }
        public Boolean Validate(SignUpRequest signUpRequest) 
        {
            if(signUpRequest.Name == null || signUpRequest.EmailId == null)
            {
                return false;
            }

            return true;
          
        }

        public async Task<Boolean> IsUserNameExist(string newUserName) 
        {
            foreach(string userName in await userRepository.GetAllUserNames())
            {
                if(userName == newUserName)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<int> ConfirmUserIdentity(LogInRequest logInData)
        {

            var user = await userRepository.FetchUserData(logInData);
           

            if(user != null)
            {
                return user.UserId;
            }
            return -1;
        }

        public Boolean HasMatchingPickupAndDropoff(int startLocationId,int endLocationId,List<int> stopList)
        {
            Boolean startFound = false;
            Boolean endFound = false;

            foreach(int locationId in stopList)
            {
                if(locationId == startLocationId)
                {
                    if(!endFound)
                    {
                        startFound= true;
                    }
                }

                if(locationId == endLocationId) 
                {
                    endFound= true;
                }
            }

            return startFound && endFound;
        }

        
        public async Task<Boolean> HasRoomForPassengers(List<int> stopListIds, int rideId, int requiredSeats,int fromLocationId,int ToLocationId)
        {
            int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
            int ToLocationIndex = stopListIds.IndexOf(ToLocationId);

            List<int> seatsAtEachStop = await availableSeatsRepository.GetAvailableSeatsList(rideId,stopListIds);

            for(int i=FromLocationIndex; i<ToLocationIndex; i++)
            {
                if (seatsAtEachStop[i] < requiredSeats)
                    return false;
            }

            return true;
        }

    }
}
