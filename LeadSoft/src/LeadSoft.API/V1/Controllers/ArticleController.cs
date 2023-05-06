using AutoMapper;
using LeadSoft.API.Controllers;
using LeadSoft.API.ViewModels;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.V1.Controllers;

[Route("v{version:apiVersion}/Article/[controller]")]
[ApiVersion("1.0")]
public class ArticleController : MainController
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public ArticleController(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<GetArticleViewModel>> Get()
    {
        return _mapper.Map<IEnumerable<GetArticleViewModel>>(await _articleRepository.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetArticleViewModel>> Get(Guid id)
    {
        return _mapper.Map<GetArticleViewModel>(await _articleRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostArticleViewModel articleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var article = _mapper.Map<Article>(articleViewModel);

        var validations = new ArticleValidation();
        var result = validations.Validate(article);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _articleRepository.Create(article);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostArticleViewModel articleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbArticle = await _articleRepository.GetById(id);

        if (dbArticle == null)
        {
            return BadRequest();
        }

        var article = _mapper.Map<Article>(articleViewModel);

        article.Id = id;

        var validations = new ArticleValidation();
        var result = validations.Validate(article);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _articleRepository.Update(article);

        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostArticleViewModel articleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbArticle = await _articleRepository.GetById(id);

        if (dbArticle == null)
        {
            return BadRequest();
        }

        var article = _mapper.Map<Article>(articleViewModel);

        article.Id = id;

        var validations = new ArticleValidation();
        var result = validations.Validate(article);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _articleRepository.Update(article);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbArticle = await _articleRepository.GetById(id);

        if (dbArticle == null)
        {
            return BadRequest();
        }

        await _articleRepository.Delete(id);

        return Ok();
    }
}