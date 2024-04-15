using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;
using RentalCompany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Interfaces
{
    public interface IOrderService
    {
        RentalPlanOutput GetSimulationPlan(RentalPlanInput input);
        Task<CreateOrderOutput> CreateOrder(CreateOrderInput input);
        List<MotorcycleOutput?> GetAllMotorcyclesAvailable();
        Task<FinishOrderOutput> FinishOrder(string id);
        Task<OrderOutput?> GetById(string id);
        Task<List<OrderOutput>> GetByMotorcycleId(string motorcycleId);
        Task<List<OrderOutput>> GetByDeliveryManId(string deliveryManId);
    }
}
