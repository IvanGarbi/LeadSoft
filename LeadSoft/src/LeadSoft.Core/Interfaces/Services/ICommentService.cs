using LeadSoft.Core.Models;

namespace LeadSoft.Core.Interfaces.Services;

public interface ICommentService : IDisposable
{
    Task Create(Comment comment);
    Task Update(Comment comment);
    Task Delete(Guid id);
}