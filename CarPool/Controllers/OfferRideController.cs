using CarPool.IServices;
using CarPool.Models;
using CarPool.Models.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    public class OfferRideController : ControllerBase
    {
        ICarpoolOfferService offerRideSerice;
        CarPoolDBContext carPoolDBContext;
        public OfferRideController(CarPoolDBContext _carPoolDBContext, ICarpoolOfferService _offerRideSerice)
        {
            carPoolDBContext = _carPoolDBContext;
            offerRideSerice= _offerRideSerice;
        }

        [HttpGet]
        public IEnumerable<OfferedRides> ShowAllRieds()
        {
            
            return carPoolDBContext.AvailableRides;
        }
        
        [HttpPost]
        public ActionResult GetOfferRideDetails(OfferRideData offerRideData)
        {
            string status = offerRideSerice.TakeRideOffer(offerRideData);
            
            return Ok(status);
            
        }
    }
}
