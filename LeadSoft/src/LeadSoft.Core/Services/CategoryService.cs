using LeadSoft.Core.Interfaces.Notifications;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;

namespace LeadSoft.Core.Services;

public class CategoryService : MainService, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IArticleRepository _articleRepository;

    public CategoryService(ICategoryRepository categoryRepository, INotify notify, IArticleRepository articleRepository) : base(notify)
    {
        _categoryRepository = categoryRepository;
        _articleRepository = articleRepository;
    }

    public async Task Create(Category category)
    {
        var validation = Validate(new CategoryValidation(), category);

        if (!validation)
        {
            return;
        }

        var dbCategory = await _categoryRepository.Get();

        var dbArticle = await _articleRepository.GetById(category.ArticleId);

        if (dbArticle == null)
        {
            Notify("This article does not exists.");

            return;
        }

        if (dbCategory.Where(x => x.ArticleId == category.ArticleId).Any())
        {
            Notify("This article already has a category.");

            return;
        }

        await _categoryRepository.Create(category);
    }

    public async Task Update(Category category)
    {
        var validation = Validate(new CategoryValidation(), category);

        if (!validation)
        {
            return;
        }

        var dbCategory = await _categoryRepository.GetById(category.Id);

        var dbArticle = await _articleRepository.GetById(category.ArticleId);

        if (dbArticle == null)
        {
            Notify("This article does not exists.");

            return;
        }

        if (dbCategory == null)
        {
            Notify("This category does not exists.");

            return;
        }

        await _categoryRepository.Update(category);
    }

    public async Task Delete(Guid id)
    {
        var dbCategory = await _categoryRepository.GetById(id);

        if (dbCategory == null)
        {
            Notify("This category does not exists.");

            return;
        }

        await _categoryRepository.Delete(id);
    }

    public async void Dispose()
    {
        _categoryRepository?.Dispose();
    }
}