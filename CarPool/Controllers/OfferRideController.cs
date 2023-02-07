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
        
        public OfferRideController(ICarpoolOfferService _offerRideSerice)
        {
           
            offerRideSerice= _offerRideSerice;
        }

      /*  [HttpGet]
        public IEnumerable<OfferedRides> ShowAllRides()
        {
            return carPoolDBContext.OfferedRides;
        }*/
        
        [HttpPost]
        public ActionResult GetOfferRideDetails(OfferRideRequest offerRideData)
        {
            string status = offerRideSerice.TakeRideOffer(offerRideData);
            
            return Ok(status);
            
        }
    }
}
