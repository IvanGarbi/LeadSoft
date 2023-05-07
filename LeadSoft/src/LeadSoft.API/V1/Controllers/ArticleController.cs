using AutoMapper;
using LeadSoft.API.Controllers;
using LeadSoft.API.ViewModels;
using LeadSoft.Core.Interfaces.Notifications;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;
using LeadSoft.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.V1.Controllers;

[Route("v{version:apiVersion}/Article/[controller]")]
[ApiVersion("1.0")]
public class ArticleController : MainController
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public ArticleController(IArticleRepository articleRepository, IMapper mapper, IArticleService articleService, INotify notify) : base(notify)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IEnumerable<GetArticleViewModel>> Get()
    {
        var articles = await _articleRepository.Get();
        var articlesViewModels = _mapper.Map<IEnumerable<GetArticleViewModel>>(articles);
        return articlesViewModels;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetArticleViewModel>> Get(Guid id)
    {
        var article = await _articleRepository.GetById(id);

        if (article == null)
        {
            _notify.AddNotification(new Notification("This article does not exists."));

            return Respose();
        }

        return _mapper.Map<GetArticleViewModel>(article);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostArticleViewModel articleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var article = _mapper.Map<Article>(articleViewModel);

        await _articleService.Create(article);

        return Respose();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostArticleViewModel articleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var article = _mapper.Map<Article>(articleViewModel);

        article.Id = id;

        await _articleService.Update(article);

        return Respose();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostArticleViewModel articleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var dbArticle = await _articleRepository.GetById(id);

        if (dbArticle == null)
        {
            return BadRequest();
        }

        var article = _mapper.Map<Article>(articleViewModel);

        article.Id = id;

        await _articleService.Update(article);

        return Respose();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        await _articleService.Delete(id);

        return Respose();
    }
}