using RentalCompany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Core.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task<Order> GetById(string id);
        Task<List<Order>> GetByMotorcycleId(string motorcycleId);
        Task<List<Order>> GetByDeliveryManId(string deliveryManId);
    }
}
