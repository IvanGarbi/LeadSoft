using AutoMapper;
using FluentValidation;
using LeadSoft.API.Controllers;
using LeadSoft.API.ViewModels;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.V1.Controllers;

[Route("v{version:apiVersion}/Category/[controller]")]
[ApiVersion("1.0")]
public class CategoryController : MainController
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<GetCategoryViewModel>> Get()
    {
        return _mapper.Map<IEnumerable<GetCategoryViewModel>>(await _categoryRepository.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetCategoryViewModel>> Get(Guid id)
    {
        return _mapper.Map<GetCategoryViewModel>(await _categoryRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var article = _mapper.Map<Category>(categoryViewModel);

        var validations = new CategoryValidation();
        var result = validations.Validate(article);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _categoryRepository.Create(article);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbCategory = await _categoryRepository.GetById(id);

        if (dbCategory == null)
        {
            return BadRequest();
        }

        var category = _mapper.Map<Category>(categoryViewModel);

        category.Id = id;

        var validations = new CategoryValidation();
        var result = validations.Validate(category);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _categoryRepository.Update(category);

        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbCategory = await _categoryRepository.GetById(id);

        if (dbCategory == null)
        {
            return BadRequest();
        }

        var category = _mapper.Map<Category>(categoryViewModel);

        category.Id = id;

        var validations = new CategoryValidation();
        var result = validations.Validate(category);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _categoryRepository.Update(category);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbCategory = await _categoryRepository.GetById(id);

        if (dbCategory == null)
        {
            return BadRequest();
        }

        await _categoryRepository.Delete(id);

        return Ok();
    }
}