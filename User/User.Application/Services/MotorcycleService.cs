using System.IO.Pipes;
using User.Application.Interfaces;
using User.Application.Models.InputModels;
using User.Application.Models.Output;
using User.Core.Entities;
using User.Core.Enums;
using User.Core.Repositories;
using User.Infrastructure.MessageBus;

namespace User.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMessageBusClient _messageBus;       
        private readonly string _exchange = "motorcycle-service";
        private readonly string _routingKey = "available-motorcycles";

        public MotorcycleService(IMotorcycleRepository motorcycleRepository
                                , IHistoricRepository historicRepository
                                , IDeliveryManRepository deliveryManRepository
                                , IMessageBusClient messageBusClient)
        {
            _motorcycleRepository = motorcycleRepository;
            _messageBus = messageBusClient;
        }

        public async Task CreateMotorcycleAsync(MotorcycleInput input)
        {
            await _motorcycleRepository.AddAsync(new Motorcycle(Guid.NewGuid().ToString(),input.Year, input.Model, input.LicensePlate, 0));
            PublishAvailableMotorcycles();
        }

        public async Task DeleteMotorcycleAsync(string licensePlate)
        {
            await _motorcycleRepository.DeleteAsync(licensePlate);
        }


        public async Task<List<MotorcycleOutput>> GetAllAsync()
        {
            var motorcycles = await _motorcycleRepository.GetAllAsync();

            return motorcycles.Select(m => new MotorcycleOutput(m.Id, m.Year, m.Model, m.LicensePlate)).ToList();
        }

        public async Task<MotorcycleDetailsOutput?> GetMotorcyclesByLicensePlateAsync(string licensePlate)
        {
            var motorcycle = await _motorcycleRepository.GetByLicensePlateAsync(licensePlate) ?? null;

            if(motorcycle == null) return null;

            MotorcycleDetailsOutput? motorcycleDetails = new MotorcycleDetailsOutput(motorcycle.Id, motorcycle.Year, motorcycle.Model, motorcycle.LicensePlate);

            return motorcycleDetails;
        }

        public async Task UpdateMotorcycleAsync(MotorcycleDetailsOutput input, MotorcycleInput motorcycle, string wrongLicensePlate)
        {
            var motorcycleUpdate = new Motorcycle(input.Id, motorcycle.Year, motorcycle.Model, motorcycle.LicensePlate, 0);
            await _motorcycleRepository.UpdateAsync(motorcycleUpdate, wrongLicensePlate);
            PublishAvailableMotorcycles();
        }

        public async Task UpdateAvailabilityMotorcycle(string id, MotorcycleStatusEnum statusEnum)
        {
           await _motorcycleRepository.UpdateAvailabilityMotorcycle(id, statusEnum);
           PublishAvailableMotorcycles();
        }

        public async void PublishAvailableMotorcycles()
        {
            var motorcycles = (await _motorcycleRepository.GetAllAsync())
                                                          .Where(m => m.Status == MotorcycleStatusEnum.Available);

            _messageBus.Publish(motorcycles, _routingKey, _exchange);
        }
    }
}
