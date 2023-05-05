using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Data.Context;

namespace LeadSoft.Data.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LeadSoftDbContext context) : base(context)
        {
        }
    }
}
