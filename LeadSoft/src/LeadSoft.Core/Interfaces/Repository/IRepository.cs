using LeadSoft.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadSoft.Core.Interfaces.Repository
{
    public interface IRepository<TE> : IDisposable where TE : Entity
    {
        Task Create(TE entity);
        Task Update(TE entity);
        Task Delete(Guid id);
        Task<TE> GetById(Guid id);
        Task<IEnumerable<TE>> Get();
        Task<int> SaveChanges();
    }
}
