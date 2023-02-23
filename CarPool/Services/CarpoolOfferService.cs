using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
using System.Globalization;

namespace CarPool.Services
{
    public class CarpoolOfferService:ICarpoolOfferService
    {
        IDataBaseService dataBaseService;
        public CarpoolOfferService(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }
        public string TakeRideOffer(OfferRideRequest offerRideData)
        {

            CultureInfo provider = CultureInfo.InvariantCulture;

            OfferedRides newRide = new OfferedRides();
         
            newRide.Time = offerRideData.Time;
            newRide.TotalPrice = offerRideData.TotalPrice;
            newRide.StopList = offerRideData.StopList;
            newRide.RideProviderId = offerRideData.RideProviderId;
            newRide.Date = offerRideData.Date;
            newRide.CurrentState= offerRideData.CurrentState;
            newRide.SeatsProvided = offerRideData.TotalSeats;

            int offeredRideId = dataBaseService.SaveRideOffer(newRide);

            if(offeredRideId != -1)
            {
                List<int> stopListIds = new List<int>(Array.ConvertAll(offerRideData.StopList.Split(','), int.Parse));
                if(GenerateAvailableSeatsList(stopListIds, offeredRideId, offerRideData.TotalSeats))
                {
                    return "You're all set! Your ride offer is now available for booking.";
                }
                
                dataBaseService.MakeRideInActive(newRide);
                return "Sorry, there was a problem with saving your ride offer. Please try again later.";
            }
            else
            {
                return "We're sorry, there was a problem saving your ride offer. Please try again.";
            }
        }

        public bool GenerateAvailableSeatsList(List<int> stopListIds,int offeredRideId, int TotalSeats) 
        {
            foreach(int id in stopListIds)
            {
                AvailableSeats availableseat = new AvailableSeats();

                availableseat.AvailableRideId = offeredRideId;
                availableseat.LocationId = id;
                availableseat.SeatAvailability = TotalSeats;

                if(! dataBaseService.SaveInAvailableSeats(availableseat))
                {
                    return false;
                }

            }
            return true;
        }
    }
}
