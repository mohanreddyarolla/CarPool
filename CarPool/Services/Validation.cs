using CarPool.IServices;
using CarPool.Models;

namespace CarPool.Services
{
    public class Validation:IValidation
    {
        IDataBaseService dataBaseService;
        
        public Validation(IDataBaseService _dataBaseService)
        {
            dataBaseService= _dataBaseService;
            
        }
        public Boolean ValidateNewUserRegistration(SignUpData signUpData) 
        {
            if(signUpData.Name == null || signUpData.EmailId == null || signUpData.Password != signUpData.ConformPassword)
            {
                return false;
            }

            return true;
          
        }

        public Boolean ConfirmUserIdentity(LogInData logInData)
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
