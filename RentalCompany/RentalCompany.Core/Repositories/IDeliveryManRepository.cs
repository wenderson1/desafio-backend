using RentalCompany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Core.Repositories
{
    public interface IDeliveryManRepository
    {
        Task AddAsync(DeliveryMan deliveryMan);
        Task UpdateAsync(DeliveryMan deliveryMan, string cnhNumber);
        Task DeleteAsync(string cnhNumber);    
        Task<DeliveryMan> GetByCnhNumber(string cnhNumber);
    }
}
