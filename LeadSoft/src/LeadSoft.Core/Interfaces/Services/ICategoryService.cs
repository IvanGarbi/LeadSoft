using LeadSoft.Core.Models;

namespace LeadSoft.Core.Interfaces.Services;

public interface ICategoryService
{
    Task Create(Category category);
    Task Update(Category category);
    Task Delete(Guid id);
}