using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LeadSoft.Data.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(LeadSoftDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Article>> Get()
        {
            return await _dbSet.AsNoTracking()
                               .Include(x => x.Author)
                               .Include(z => z.Comments)
                               .Include(c => c.Category)
                               .ToListAsync();
        }
    }
}
