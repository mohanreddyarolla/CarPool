using Carpool.Models.DBModels;

namespace CarPool.Interface.IRepository
{
    public interface ILocationsRepository
    {
        public Task<string> GetLocationById(int id);
        public Task<List<Location>> GetLocations();
    }
}
