using User.Application.Models.InputModels;
using User.Application.Models.Output;
using User.Core.Entities;
using User.Core.Enums;

namespace User.Application.Interfaces
{
    public interface IMotorcycleService
    {
        Task CreateMotorcycleAsync(MotorcycleInput input);
        Task<MotorcycleDetailsOutput?> GetMotorcyclesByLicensePlateAsync(string licensePlate);
        Task UpdateMotorcycleAsync(MotorcycleDetailsOutput result, MotorcycleInput motorcycle, string wrongLicensePlate);
        Task DeleteMotorcycleAsync(string licensePlate);
        Task<List<MotorcycleOutput>> GetAllAsync();
        Task UpdateAvailabilityMotorcycle(string id, MotorcycleStatusEnum statusEnum);
    }
}
