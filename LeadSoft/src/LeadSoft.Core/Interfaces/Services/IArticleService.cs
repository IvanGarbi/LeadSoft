using LeadSoft.Core.Models;

namespace LeadSoft.Core.Interfaces.Services;

public interface IArticleService
{
    Task Create(Article article);
    Task Update(Article article);
    Task Delete(Guid id);
    Task ReadById(Guid id);
}