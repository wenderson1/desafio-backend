using MongoDB.Bson;
using MongoDB.Driver;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Infrastructure.Persistence.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly IMongoCollection<Motorcycle> _collection;
        public MotorcycleRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Motorcycle>("motorcycle");
        }

        public async Task AddAsync(Motorcycle product)
        {
            await _collection.InsertOneAsync(product);
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
    }
}

