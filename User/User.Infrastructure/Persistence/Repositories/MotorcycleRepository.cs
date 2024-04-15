using MongoDB.Bson;
using MongoDB.Driver;
using User.Core.Entities;
using User.Core.Enums;
using User.Core.Repositories;
using static MongoDB.Driver.WriteConcern;

namespace User.Infrastructure.Persistence.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly IMongoCollection<Motorcycle> _collection;
        public MotorcycleRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Motorcycle>("motorcycle");
        }

        public async Task AddAsync(Motorcycle motorcycle)
        {
            await _collection.InsertOneAsync(motorcycle);
        }

        public async Task DeleteAsync(string licensePlate)
        {
            var filter = Builders<Motorcycle>.Filter.And(
                         Builders<Motorcycle>.Filter.Where(m => m.LicensePlate == licensePlate));

            await _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<List<Motorcycle>> GetAllAsync()
        {
            return await _collection.Find(m => true).ToListAsync();
        }

        public async Task<Motorcycle> GetByLicensePlateAsync(string licensePlate)
        {
            return await _collection.Find(m => m.LicensePlate == licensePlate).SingleOrDefaultAsync();
        }



        public async Task UpdateAsync(Motorcycle newLicensePlate, string wrongLicensePlate)
        {
            await _collection.ReplaceOneAsync(m=> m.LicensePlate == wrongLicensePlate, newLicensePlate);
        }

        public async Task UpdateAvailabilityMotorcycle(string id, MotorcycleStatusEnum statusEnum)
        {
            var filter = Builders<Motorcycle>.Filter.Eq("_id", id);
           
            var update = Builders<Motorcycle>.Update.Set("Status", statusEnum);

          await  _collection.UpdateOneAsync(filter, update);
        }
    }
}

