using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;
using RentalCompany.Core.Entities;

namespace RentalCompany.Application.Interfaces
{
    public interface IDeliveryManService
    {
        Task CreateDeliveryMan(DeliveryManInput deliveryMan);
        Task UpdateAsync(DeliveryManUpdateInput deliveryMan, string cnhNumber);
        Task UpdateCnhImage(UploadCnhImageInput image);    
        Task DeleteAsync(string cnhNumber);
        Task<DeliveryManOutput?>GetByCnhNumber(string cnh);
        Task<DeliveryManDetailsOutput?>GetDetailsByCnhNumber(string cnhNumber);
    }
}
