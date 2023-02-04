using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.IServices
{
    public interface ICarpoolOfferService
    {
        public string TakeRideOffer(OfferRideData offerRideData);
        public bool GenerateAvailableSeatsList(List<int> stopListIds, int offeredRideId, int TotalSeats);
    }
}
