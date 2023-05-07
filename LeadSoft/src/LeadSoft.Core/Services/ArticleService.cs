using LeadSoft.Core.Interfaces.Notifications;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;

namespace LeadSoft.Core.Services;

public class ArticleService : MainService, IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IAuthorRepository _authorRepository;
    
    public ArticleService(IArticleRepository articleRepository, INotify notify, IAuthorRepository authorRepository) : base(notify)
    {
        _articleRepository = articleRepository;
        _authorRepository = authorRepository;
    }

    public async Task Create(Article article)
    {
        var validation = Validate(new ArticleValidation(), article);

        if (!validation)
        {
            return;
        }

        var dbAuthor = await _authorRepository.GetById(article.AuthorId);

        if (dbAuthor == null)
        {
            Notify("This author does not exists.");

            return;
        }

        await _articleRepository.Create(article);
    }

    public async Task Update(Article article)
    {
        var validation = Validate(new ArticleValidation(), article);

        if (!validation)
        {
            return;
        }

        var dbArticle = await _articleRepository.GetById(article.Id);

        var dbAuthor = await _authorRepository.GetById(article.AuthorId);

        if (dbArticle == null)
        {
            Notify("This article does not exists.");

            return;
        }

        if (dbAuthor == null)
        {
            Notify("This author does not exists.");

            return;
        }

        await _articleRepository.Update(article);
    }

    public async Task Delete(Guid id)
    {
        var dbArticle = await _articleRepository.GetById(id);

        if (dbArticle == null)
        {
            Notify("This article does not exists.");

            return;
        }

        await _articleRepository.Delete(id);
    }


    public async void Dispose()
    {
        _articleRepository?.Dispose();
    }
}