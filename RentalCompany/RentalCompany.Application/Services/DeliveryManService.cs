using RentalCompany.Application.Interfaces;
using RentalCompany.Application.Models.Input;
using RentalCompany.Application.Models.Output;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Repositories;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;

namespace RentalCompany.Application.Services
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly string _basePath = @"C:\CnhImages\";

        public DeliveryManService(IDeliveryManRepository deliveryManRepository)
        {
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task CreateDeliveryMan(DeliveryManInput input)
        {
            await _deliveryManRepository.AddAsync(new DeliveryMan(input.Name, input.CNPJ, input.BirthDate, input.CnhNumber, input.CnhType));
        }

        public async Task DeleteAsync(string cnhNumber)
        {
            await _deliveryManRepository.DeleteAsync(cnhNumber);
        }

        public async Task<DeliveryManOutput?> GetByCnhNumber(string cnh)
        {
            var result = await _deliveryManRepository.GetByCnhNumber(cnh);

            if (result == null) return null;

            return new DeliveryManOutput(result.Id, result.Name, result.CNPJ, result.BirthDate, result.CnhNumber, result.CnhType);
        }

        public async Task UpdateAsync(DeliveryManUpdateInput input, string cnhNumber)
        {
            var deliveryManUpdate = new DeliveryMan(input.Name, input.CNPJ, input.BirthDate, input.CnhType);
            await _deliveryManRepository.UpdateAsync(deliveryManUpdate, cnhNumber);
        }

        public async Task UpdateCnhImage(UploadCnhImageInput input)
        {
            await SaveLocalImage(input);
        }

        public async Task SaveLocalImage(UploadCnhImageInput input)
         {
            string fileName = $"{input.CnhNumber}{input.CnhImage.FileName.Substring(input.CnhImage.FileName.Length - 4)}";
            string filePath = Path.Combine(_basePath, fileName);


            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await input.CnhImage.CopyToAsync(fileStream);
            }
        }

        public byte[] GetLocalImage(string cnhNumber)
        {
            var imagePath = @"{ _basePath }{ cnhNumber}";

            if (File.Exists($"{imagePath}.png"))
            {
                byte[] imageBytes = File.ReadAllBytes($"{imagePath}.png");
                return imageBytes;
            }
            if (File.Exists($"{imagePath}.bmp"))
            {
                byte[] imageBytes = File.ReadAllBytes($"{imagePath}.bmp");
                return imageBytes;
            }
            return [];
        }

        public async Task<DeliveryManDetailsOutput?> GetDetailsByCnhNumber(string cnhNumber)
        {
            var result = await _deliveryManRepository.GetByCnhNumber(cnhNumber);

            if (result == null) return null;

            var image = this.GetLocalImage(cnhNumber);

            return new DeliveryManDetailsOutput(result.Id, 
                                                result.Name, 
                                                result.CNPJ,
                                                result.BirthDate,
                                                result.CnhNumber,
                                                result.CnhType, 
                                                image);
        }
    }
}

