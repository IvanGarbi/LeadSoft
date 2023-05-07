using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LeadSoft.Data.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LeadSoftDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Author>> Get()
        {
            return await _dbSet.AsNoTracking()
                               .Include(x => x.Articles)
                               .ToListAsync();
        }
    }
}
