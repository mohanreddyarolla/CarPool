using CarPool.Interface;
using CarPool.Interface.IRepository;
using Carpool.Models;
using Carpool.Models.DBModels;
using System.Globalization;

namespace CarPool.Services
{
    public class CarpoolOfferService:ICarpoolOfferService
    {
     
        IOfferedRidesRepository offeredRidesRepository;
        IAvailableSeatsRepository availableSeatsRepository;
        public CarpoolOfferService(IAvailableSeatsRepository availableSeatsRepository, IOfferedRidesRepository _offeredRidesRepository)
        {
            this.availableSeatsRepository = availableSeatsRepository;
            offeredRidesRepository= _offeredRidesRepository;
        }
        public async Task<string> TakeRideOffer(OfferRideRequest offerRideData)
        {

            CultureInfo provider = CultureInfo.InvariantCulture;

            OfferedRide newRide = new OfferedRide();
         
            newRide.Time = offerRideData.Time;
            newRide.TotalPrice = offerRideData.TotalPrice;
            newRide.StopList = offerRideData.StopList;
            newRide.RideProviderId = offerRideData.RideProviderId;
            newRide.Date = offerRideData.Date;
            newRide.CurrentState= offerRideData.CurrentState;
            newRide.SeatsProvided = offerRideData.TotalSeats;

            int offeredRideId = await offeredRidesRepository.SaveRideOffer(newRide);

            if(offeredRideId != -1)
            {
                List<int> stopListIds = new List<int>(Array.ConvertAll(offerRideData.StopList.Split(','), int.Parse));
                if(await GenerateAvailableSeatsList(stopListIds, offeredRideId, offerRideData.TotalSeats))
                {
                    return "You're all set! Your ride offer is now available for booking.";
                }
                
                await  offeredRidesRepository.MakeRideInActive(newRide);
                return "Sorry, there was a problem with saving your ride offer. Please try again later.";
            }
            else
            {
                return "We're sorry, there was a problem saving your ride offer. Please try again.";
            }
        }

        public async Task<bool> GenerateAvailableSeatsList(List<int> stopListIds,int offeredRideId, int TotalSeats) 
        {
            foreach(int id in stopListIds)
            {
                AvailableSeats availableseat = new AvailableSeats();

                availableseat.AvailableRideId = offeredRideId;
                availableseat.LocationId = id;
                availableseat.SeatAvailability = TotalSeats;

                if(! await availableSeatsRepository.SaveInAvailableSeats(availableseat))
                {
                    return false;
                }

            }
            return true;
        }
    }
}
