
using RentalCompany.Core.Entities;

namespace RentalCompany.Core.CacheStorage
{
    public interface ICacheService
    {
        void SetCache(string key, List<Motorcycle> data);
        List<Motorcycle> GetMotorcycleList();
    }
}
