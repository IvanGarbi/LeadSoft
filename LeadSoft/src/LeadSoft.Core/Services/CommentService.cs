using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;

namespace LeadSoft.Core.Services;

public class CommentService : MainService, ICommentService
{
    public Task Create(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task Update(Comment comment)
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