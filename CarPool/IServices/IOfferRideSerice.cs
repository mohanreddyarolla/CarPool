using CarPool.Models;
using CarPool.Models.DBModels;

namespace CarPool.IServices
{
    public interface IOfferRideSerice
    {
        public Boolean SaveRideOffer(OfferRideData offerRideData);
    }
}
