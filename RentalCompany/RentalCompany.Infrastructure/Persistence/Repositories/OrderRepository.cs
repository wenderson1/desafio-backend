using MongoDB.Driver;
using RentalCompany.Core.Entities;
using RentalCompany.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _collection;
        public OrderRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Order>("Order");
        }
        public async Task AddAsync(Order order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task<List<Order>> GetByDeliveryManId(string deliveryManId)
        {
            return await _collection.Find(FilterDeliveryMan(deliveryManId)).ToListAsync();
        }

        public async Task<Order> GetById(string id)
        {
            return await _collection.Find(o => o.Id== id).SingleOrDefaultAsync();
        }

        public async Task<List<Order>> GetByMotorcycleId(string motorcycleId)
        {
            return await _collection.Find(FilterMotorcycle(motorcycleId)).ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _collection.ReplaceOneAsync(m => m.Id == order.Id, order);
        }

        public static FilterDefinition<Order> Filter(string id)
        {
            var filter = Builders<Order>.Filter.And(
             Builders<Order>.Filter.Where(d => d.Id == id));

            return filter;
        }

        public static FilterDefinition<Order> FilterDeliveryMan(string idDeliveryMan)
        {
            var filter = Builders<Order>.Filter.And(
             Builders<Order>.Filter.Where(d => d.IdDeliveryMan == idDeliveryMan));

            return filter;
        }
        public static FilterDefinition<Order> FilterMotorcycle(string idMotorcycle)
        {
            var filter = Builders<Order>.Filter.And(
             Builders<Order>.Filter.Where(d => d.IdMotorcycle == idMotorcycle));

            return filter;
        }

    }
}
