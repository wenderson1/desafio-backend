using MongoDB.Driver;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Infrastructure.Persistence.Repositories
{
    public class HistoricRepository : IHistoricRepository
    {
        private readonly IMongoCollection<Historic> _collection;
        public HistoricRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Historic>("historics");
        }
        public async Task<List<Historic>> GetMotorcycleHistoricsAsync(List<string> idHistorics)
        {
            var filter = Builders<Historic>.Filter.And(
                         Builders<Historic>.Filter.Where(h => idHistorics.Contains(h.Id)));

            return await _collection.FindSync<Historic>(filter).ToListAsync();
        }
    }
}
