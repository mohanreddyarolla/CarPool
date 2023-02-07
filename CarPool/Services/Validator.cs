using CarPool.IServices;
using CarPool.Models;

namespace CarPool.Services
{
    public class Validator:IValidator
    {
        IDataBaseService dataBaseService;
        
        public Validator(IDataBaseService _dataBaseService)
        {
            dataBaseService= _dataBaseService;
            
        }
        public Boolean Validate(SignUpRequest signUpRequest) 
        {
            if(signUpRequest.Name == null || signUpRequest.EmailId == null || signUpRequest.Password != signUpRequest.ConformPassword)
            {
                return false;
            }

            return true;
          
        }

        public Boolean IsUserNameExist(string newUserName) 
        {
            foreach(string userName in dataBaseService.GetAllUserNames())
            {
                if(userName == newUserName)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean ConfirmUserIdentity(LogInRequest logInData)
        {
            var user = dataBaseService.FetchUserData(logInData);

            if(user != null)
            {
                return true;
            }
            return false;
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

        
        public Boolean HasRoomForPassengers(List<int> stopListIds, int rideId, int requiredSeats,int fromLocationId,int ToLocationId)
        {
            int FromLocationIndex = stopListIds.IndexOf(fromLocationId);
            int ToLocationIndex = stopListIds.IndexOf(ToLocationId);

            List<int> seatsAtEachStop = dataBaseService.GetAvailableSeatsList(rideId,stopListIds);

            for(int i=FromLocationIndex; i<ToLocationIndex; i++)
            {
                if (seatsAtEachStop[i] < requiredSeats)
                    return false;
            }

            return true;
        }

    }
}
