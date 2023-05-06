using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;

namespace LeadSoft.Core.Services;

public class ArticleService : MainService, IArticleService
{
    public Task Create(Article article)
    {
        throw new NotImplementedException();
    }

    public Task Update(Article article)
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