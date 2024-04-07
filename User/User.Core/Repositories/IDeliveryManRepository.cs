using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IDeliveryManRepository
    {
        Task<DeliveryMan> GetDeliveryManHistoricAsync(string idHistoric);
    }
}
