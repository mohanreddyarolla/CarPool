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
        CarPoolDBContext carPoolDBContext;
        public OfferRideController(CarPoolDBContext _carPoolDBContext, IOfferRideSerice _offerRideSerice)
        {
            carPoolDBContext = _carPoolDBContext;
            offerRideSerice= _offerRideSerice;
        }

        [HttpGet]
        public IEnumerable<AvailableRides> ShowAllRieds()
        {
            
            return carPoolDBContext.AvailableRides;
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
