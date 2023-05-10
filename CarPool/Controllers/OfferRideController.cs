using CarPool.Interface;
using Carpool.Models;
using Carpool.Models.DBModels;
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
        public IEnumerable<OfferedRide> ShowAllRides()
        {
            return carPoolDBContext.OfferedRide;
        }*/
        
        [HttpPost]
        public async Task<ActionResult> GetOfferRideDetails(OfferRideRequest offerRideData)
        {
            string status = await offerRideSerice.TakeRideOffer(offerRideData);
            
            return Ok(JsonSerializer.Serialize(status));
            
        }
    }
}
