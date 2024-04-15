using User.Core.Entities;
using User.Core.Enums;

namespace User.Core.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<List<Motorcycle>> GetAllAsync();
        Task<Motorcycle> GetByLicensePlateAsync(string licensePlate);
        Task AddAsync(Motorcycle motorcycle);
        Task UpdateAsync(Motorcycle newLicensePlate, string wrongLicensePlate);
        Task DeleteAsync(string licensePlate);
        Task UpdateAvailabilityMotorcycle(string id, MotorcycleStatusEnum statusEnum);
    }
}
