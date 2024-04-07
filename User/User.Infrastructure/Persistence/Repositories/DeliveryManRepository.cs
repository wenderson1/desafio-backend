using MongoDB.Driver;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Infrastructure.Persistence.Repositories
{
    public class DeliveryManRepository : IDeliveryManRepository
    {
        private readonly IMongoCollection<DeliveryMan> _collection;
        public DeliveryManRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<DeliveryMan>("deliveryMan");
        }

        public async Task<DeliveryMan> GetDeliveryManHistoricAsync(string idDeliverMan)
        {
            return await _collection.Find(dm => dm.Id == idDeliverMan).SingleOrDefaultAsync();
        }
    }
}
