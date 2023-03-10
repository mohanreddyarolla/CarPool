using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarPool.Controllers
{
    [Route("api/CarPool/[controller]")]
    [ApiController]
    [Authorize]
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
            
            return Ok(JsonSerializer.Serialize(status));
            
        }
    }
}
