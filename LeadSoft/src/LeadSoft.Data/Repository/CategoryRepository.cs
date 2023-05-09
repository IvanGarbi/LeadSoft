using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LeadSoft.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(LeadSoftDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Category>> Get()
        {
            return await _dbSet.AsNoTracking()
                               .Include(x => x.Article)
                               .ToListAsync();
        }

        public virtual async Task<Category> GetByIdWithRelations(Guid id)
        {
            return await _dbSet.AsNoTracking()
                               .Where(x => x.Id == id)
                               .Include(i => i.Article)
                               .FirstOrDefaultAsync();
        }
    }
}
