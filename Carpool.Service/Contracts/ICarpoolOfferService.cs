using Carpool.Models;
using Carpool.Models.DBModels;

namespace CarPool.Interface
{
    public interface ICarpoolOfferService
    {
        public Task<string> TakeRideOffer(OfferRideRequest offerRideData);
        public Task<bool> GenerateAvailableSeatsList(List<int> stopListIds, int offeredRideId, int TotalSeats);
    }
}
