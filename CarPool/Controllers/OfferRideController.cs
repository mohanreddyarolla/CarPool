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
        IOfferRideSerice offerRideSerice;
        public OfferRideController(IOfferRideSerice _offerRideSerice)
        {
            offerRideSerice= _offerRideSerice;
        }
        
        [HttpPost]
        public ActionResult GetOfferRideDetails(OfferRideData offerRideData)
        {
            Console.WriteLine("InController");
            if (offerRideSerice.SaveRideOffer(offerRideData))
            {
                return Ok("Ride Offered Successfully");
            }

            return BadRequest("Ride offer Rejected");
            
        }
    }
}
