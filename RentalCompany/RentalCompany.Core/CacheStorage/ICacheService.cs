
using RentalCompany.Core.Entities;

namespace RentalCompany.Core.CacheStorage
{
    public interface ICacheService
    {
        void SetCache(List<Motorcycle> data);
        List<Motorcycle> GetMotorcycleList();
        void Remove(string key);
    }
}
