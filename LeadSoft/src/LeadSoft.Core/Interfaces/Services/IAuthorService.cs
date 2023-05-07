using LeadSoft.Core.Models;

namespace LeadSoft.Core.Interfaces.Services;

public interface IAuthorService
{
    Task Create(Author author);
    Task Update(Author author);
    Task Delete(Guid id);
}