using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;

namespace LeadSoft.Core.Services;

public class AuthorService : MainService, IAuthorService
{
    public Task Create(Author author)
    {
        throw new NotImplementedException();
    }

    public Task Update(Author author)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task ReadById(Guid id)
    {
        throw new NotImplementedException();
    }
}