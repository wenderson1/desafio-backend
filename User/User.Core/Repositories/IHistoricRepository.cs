using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IHistoricRepository
    {
        Task<List<Historic>> GetMotorcycleHistoricsAsync(List<string> idHistorics);
    }
}
