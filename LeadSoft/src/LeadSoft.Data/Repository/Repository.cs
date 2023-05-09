using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LeadSoft.Data.Repository
{
    public class Repository<TE> : IRepository<TE> where TE : Entity, new()
    {
        protected readonly LeadSoftDbContext _context;
        protected readonly DbSet<TE> _dbSet;

        public Repository(LeadSoftDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TE>();
        }

        public async Task Create(TE entity)
        {
            _context.Add(entity);
            await SaveChanges();
        }

        public virtual async Task<IEnumerable<TE>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TE> GetById(Guid id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TE> GetByIdWithRelations(Guid id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(TE entity)
        {
            _context.Update(entity);
            await SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            _context.Remove(new TE { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            _context?.Dispose();
        }
    }
}
