using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;

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

            AvailableRides newRide = new AvailableRides();
            newRide.StartTime = offerRideData.StartTime;
            newRide.EndTime = offerRideData.EndTime;
            newRide.TotalPrice = offerRideData.TotalPrice;
            newRide.StopList = offerRideData.StopList;
            newRide.UserId = offerRideData.UserId;
            newRide.Date = offerRideData.Date;

            return dataBaseService.SaveInAvailableRides(newRide);
        }
    }
}
