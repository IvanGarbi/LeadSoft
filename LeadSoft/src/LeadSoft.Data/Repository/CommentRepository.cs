using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LeadSoft.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(LeadSoftDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Comment>> Get()
        {
            return await _dbSet.AsNoTracking()
                               .Include(x => x.Article)
                               .ToListAsync();
        }
    }
}
