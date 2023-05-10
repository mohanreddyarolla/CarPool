using CarPool.Interface.IRepository;
using Carpool.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Repository
{
    public class LocationsRepository:ILocationsRepository
    {
        CarPoolDBContext carPoolDBContext;

        public LocationsRepository(CarPoolDBContext _carPoolDBContext)
        {
            carPoolDBContext = _carPoolDBContext;

        }


        public async Task<List<Location>> GetLocations()
        {
            try
            {
                List<Location> locations = await carPoolDBContext.Location.ToListAsync();
                return locations;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetLocationById(int id)
        { 
            var name = await carPoolDBContext.Location.FirstOrDefaultAsync(location => location.LocationId == id);
            if (name != null)
            {
                return name.LocationName;
            }
            return "";

        }
    }
}
