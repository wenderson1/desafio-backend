using System.Collections.Immutable;
using User.Application.Interfaces;
using User.Application.Models.InputModels;
using User.Application.Models.Output;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IHistoricRepository _historyRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository
                                , IHistoricRepository historicRepository
                                , IDeliveryManRepository deliveryManRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _historyRepository = historicRepository;
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task CreateMotorcycleAsync(MotorcycleInput input)
        {
            await _motorcycleRepository.AddAsync(new Motorcycle(Guid.NewGuid().ToString(),input.Year, input.Model, input.LicensePlate));
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

            if (motorcycle.IdHistoric != null)
            {
                var historics = await _historyRepository.GetMotorcycleHistoricsAsync(motorcycle.IdHistoric);
                var historicList = historics.Select(h => new HistoricOutput(h.Id, h.WithdrawalDate, h.ReturnDate)).ToList();

                for (int i = 0; i <= historics.Count() - 1; i++)
                {
                    historicList[i].DeliveryMan = await _deliveryManRepository.GetDeliveryManHistoricAsync(historics.ElementAt(i).IdDeliveryMan);
                }

                motorcycleDetails.Historic.AddRange(historicList);
            }

            return motorcycleDetails;
        }

        public async Task UpdateMotorcycleAsync(MotorcycleDetailsOutput input, MotorcycleInput motorcycle, string wrongLicensePlate)
        {
            var motorcycleUpdated = new Motorcycle(input.Id, motorcycle.Year, motorcycle.Model, motorcycle.LicensePlate);

            await _motorcycleRepository.UpdateAsync(motorcycleUpdated, wrongLicensePlate);
        }
    }
}
