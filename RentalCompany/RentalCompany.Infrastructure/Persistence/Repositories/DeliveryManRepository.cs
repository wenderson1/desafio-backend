using MongoDB.Driver;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Repositories;

namespace RentalCompany.Infrastructure.Persistence.Repositories
{
    public class DeliveryManRepository : IDeliveryManRepository
    {
        private readonly IMongoCollection<DeliveryMan> _collection;
        public DeliveryManRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<DeliveryMan>("deliveryMan");
        }
        public async Task AddAsync(DeliveryMan deliveryMan)
        {
            await _collection.InsertOneAsync(deliveryMan);
        }

        public async Task DeleteAsync(string cnhNumber)
        {
            await _collection.FindOneAndDeleteAsync(Filter(cnhNumber));
        }

        public async Task UpdateAsync(DeliveryMan deliveryMan, string cnhNumber)
        {
            await _collection.ReplaceOneAsync(Filter(cnhNumber), deliveryMan);
        }

        public static FilterDefinition<DeliveryMan> Filter(string cnhNumber)
        {
            var filter = Builders<DeliveryMan>.Filter.And(
             Builders<DeliveryMan>.Filter.Where(d => d.CnhNumber == cnhNumber));

            return filter;
        }

        public async Task<DeliveryMan> GetByCnhNumber(string cnhNumber)
        {
            return await _collection.Find(Filter(cnhNumber)).SingleOrDefaultAsync();
        }
    }
}
