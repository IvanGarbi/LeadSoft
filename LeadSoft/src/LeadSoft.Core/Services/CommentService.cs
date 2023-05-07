using LeadSoft.Core.Interfaces.Notifications;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;

namespace LeadSoft.Core.Services;

public class CommentService : MainService, ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleRepository _articleRepository;

    public CommentService(ICommentRepository commentRepository, INotify notify, IArticleRepository articleRepository) : base(notify)
    {
        _commentRepository = commentRepository;
        _articleRepository = articleRepository;
    }

    public async Task Create(Comment comment)
    {
        var validation = Validate(new CommentValidation(), comment);

        if (!validation)
        {
            return;
        }

        var dbArticle = await _articleRepository.GetById(comment.ArticleId);

        if (dbArticle == null)
        {
            Notify("This article does not exists.");

            return;
        }

        await _commentRepository.Create(comment);
    }

    public async Task Update(Comment comment)
    {
        var validation = Validate(new CommentValidation(), comment);

        if (!validation)
        {
            return;
        }

        var dbComment = await _commentRepository.GetById(comment.Id);

        var dbArticle = await _articleRepository.GetById(comment.ArticleId);

        if (dbComment == null)
        {
            Notify("This comment does not exists.");

            return;
        }

        if (dbArticle == null)
        {
            Notify("This article does not exists.");

            return;
        }

        await _commentRepository.Update(comment);
    }

    public async Task Delete(Guid id)
    {
        var dbComment = await _commentRepository.GetById(id);

        if (dbComment == null)
        {
            Notify("This article does not exists.");

            return;
        }

        await _commentRepository.Delete(id);
    }

    public async void Dispose()
    {
        _commentRepository?.Dispose();
    }
}