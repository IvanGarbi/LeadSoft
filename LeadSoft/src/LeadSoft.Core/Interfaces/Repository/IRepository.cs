using LeadSoft.Core.Models;

namespace LeadSoft.Core.Interfaces.Repository
{
    public interface IRepository<TE> : IDisposable where TE : Entity
    {
        Task Create(TE entity);
        Task Update(TE entity);
        Task Delete(Guid id);
        Task<TE> GetById(Guid id);
        Task<TE> GetByIdWithRelations(Guid id);
        Task<IEnumerable<TE>> Get();
        Task<int> SaveChanges();
    }
}
