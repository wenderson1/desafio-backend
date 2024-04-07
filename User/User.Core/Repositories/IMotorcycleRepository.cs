using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<List<Motorcycle>> GetAllAsync();
        Task<Motorcycle> GetByLicensePlateAsync(string licensePlate);
        Task AddAsync(Motorcycle motorcycle);
        Task UpdateAsync(Motorcycle newLicensePlate, string wrongLicensePlate);
        Task DeleteAsync(string licensePlate);
    }
}
