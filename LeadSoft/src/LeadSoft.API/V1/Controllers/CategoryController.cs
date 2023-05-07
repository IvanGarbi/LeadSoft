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

[Route("v{version:apiVersion}/Category/[controller]")]
[ApiVersion("1.0")]
public class CategoryController : MainController
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, ICategoryService categoryService, INotify notify) : base(notify)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IEnumerable<GetCategoryViewModel>> Get()
    {
        var categories = await _categoryRepository.Get();
        var categoriesViewModels = _mapper.Map<IEnumerable<GetCategoryViewModel>>(categories);
        return categoriesViewModels;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetCategoryViewModel>> Get(Guid id)
    {
        var category = await _categoryRepository.GetById(id);

        if (category == null)
        {
            _notify.AddNotification(new Notification("This category does not exists."));

            return Respose();
        }

        return _mapper.Map<GetCategoryViewModel>(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var article = _mapper.Map<Category>(categoryViewModel);

        await _categoryService.Create(article);

        return Respose();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var dbCategory = await _categoryRepository.GetById(id);

        if (dbCategory == null)
        {
            return BadRequest();
        }

        var category = _mapper.Map<Category>(categoryViewModel);

        category.Id = id;

        await _categoryService.Update(category);

        return Respose();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var dbCategory = await _categoryRepository.GetById(id);

        if (dbCategory == null)
        {
            return BadRequest();
        }

        var category = _mapper.Map<Category>(categoryViewModel);

        category.Id = id;

        await _categoryService.Update(category);

        return Respose();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        await _categoryService.Delete(id);

        return Respose();
    }
}