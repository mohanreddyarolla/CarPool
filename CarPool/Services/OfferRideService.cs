using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;
using System.Globalization;

namespace CarPool.Services
{
    public class OfferRideService:IOfferRideSerice
    {
        IDataBaseService dataBaseService;
        public OfferRideService(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }
        public Boolean SaveRideOffer(OfferRideData offerRideData)
        {
            Console.WriteLine("InService");

            CultureInfo provider = CultureInfo.InvariantCulture;

            AvailableRides newRide = new AvailableRides();
            newRide.StartTime = TimeSpan.ParseExact(offerRideData.StartTime, "g", provider);
            newRide.EndTime = TimeSpan.ParseExact(offerRideData.EndTime, "g", provider);
            newRide.TotalPrice = offerRideData.TotalPrice;
            newRide.StopList = offerRideData.StopList;
            newRide.UserId = offerRideData.UserId;
            newRide.Date = DateTime.ParseExact(offerRideData.Date, "yyyy-mm-dd", provider);
            newRide.CurrentState= offerRideData.CurrentState;

            int offeredRideId = dataBaseService.SaveInAvailableRides(newRide);

            if(offeredRideId != -1)
            {
                List<int> stopListIds = new List<int>(Array.ConvertAll(offerRideData.StopList.Split(','), int.Parse));
                if(GenerateAvailableSeatsList(stopListIds, offeredRideId, offerRideData.TotalSeats))
                {
                    return true;
                }
                newRide.CurrentState = "InActive";
                dataBaseService.MakeRideInActive(newRide);
                return false;
            }
            else
            {
                return false;
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
