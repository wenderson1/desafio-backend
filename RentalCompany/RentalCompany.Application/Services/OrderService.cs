using RentalCompany.Application.Interfaces;
using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;
using RentalCompany.Core.CacheStorage;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Enums;
using RentalCompany.Core.Repositories;
using System.Data;

namespace RentalCompany.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICacheService _cache;
        private readonly IOrderRepository _repository;
        public OrderService(ICacheService cache, IOrderRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<CreateOrderOutput> CreateOrder(CreateOrderInput input)
        {
            var orderOutput = new CreateOrderOutput
            {
                StartDate = input.StartDate,
                ExpectedReturnDate = input.ExpectedReturnDate,
                Price = RentalPrices.CalculateTotalAmountWithoutPenalty(input.StartDate, input.ExpectedReturnDate)
            };

            var order = new Order(input.IdDeliveryMan, input.IdMotorcycle, OrderStatusEnum.Created, input.StartDate, input.ExpectedReturnDate, null, orderOutput.Price);

            //publicar mensagem na Fila para alterar Status

            await _repository.AddAsync(order);

            return orderOutput;
        }

        public async Task<FinishOrderOutput> FinishOrder(string id)
        {
            var result = await _repository.GetById(id);

            if (result.ExpectedReturnDate < DateTime.Now) result.ReturnDate = result.ExpectedReturnDate;

            result.Status = OrderStatusEnum.Finished;
            result.Price = RentalPrices.CalculateTotalAmountWithPenalty(result.StartDate, result.ExpectedReturnDate, result.ReturnDate ?? DateTime.Now);
            result.ReturnDate = DateTime.Now.Date;

            await _repository.UpdateAsync(result);

            return new FinishOrderOutput
            {
                StartDate = result.StartDate,
                ExpectedReturnDate = result.ExpectedReturnDate,
                ReturnDate = result.ReturnDate ?? DateTime.Now,
                Price = result.Price
            };
        }

        public List<MotorcycleOutput> GetAllMotorcyclesAvailable()
        {
            var result = _cache.GetMotorcycleList();

            if (result == null) return new List<MotorcycleOutput>();

            return result.Select(x => new MotorcycleOutput(x.Id, x.Year, x.Model, x.LicensePlate)).ToList();
        }

        public async Task<List<OrderOutput>> GetByDeliveryManId(string deliveryManId)
        {
            var result = await _repository.GetByDeliveryManId(deliveryManId);

            if (!result.Any()) return new List<OrderOutput>();

            return result.Select(o => new OrderOutput(o.Id,
                                                      o.IdDeliveryMan,
                                                      o.IdMotorcycle,
                                                      o.Status,
                                                      o.StartDate,
                                                      o.ExpectedReturnDate,
                                                      o.ReturnDate,
                                                      o.Price)).ToList();
        }

        public async Task<OrderOutput?> GetById(string id)
        {
            var result = await _repository.GetById(id);

            if (result == null) return null;

            return new OrderOutput(result.Id,
                                   result.IdDeliveryMan,
                                   result.IdMotorcycle,
                                   result.Status,
                                   result.StartDate,
                                   result.ExpectedReturnDate,
                                   result.ReturnDate,
                                   result.Price);
        }

        public async Task<List<OrderOutput>> GetByMotorcycleId(string motorcycleId)
        {
            var result = await _repository.GetByMotorcycleId(motorcycleId);

            if (!result.Any()) return new List<OrderOutput>();

            return result.Select(o => new OrderOutput(o.Id,
                                                      o.IdDeliveryMan,
                                                      o.IdMotorcycle,
                                                      o.Status,
                                                      o.StartDate,
                                                      o.ExpectedReturnDate,
                                                      o.ReturnDate,
                                                      o.Price)).ToList();
        }

        public RentalPlanOutput GetSimulationPlan(RentalPlanInput input)
        {
            var expectedPrice = RentalPrices.CalculateTotalAmountWithoutPenalty(input.StartDate, input.ExpectedReturnDate);

            return new RentalPlanOutput
            {
                ExpectedPrice = expectedPrice,
                StartDate = input.StartDate,
                ExpectedReturnDate = input.ExpectedReturnDate
            };
        }
    }
}
