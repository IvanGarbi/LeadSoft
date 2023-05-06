using AutoMapper;
using LeadSoft.API.Controllers;
using LeadSoft.API.ViewModels;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.V1.Controllers;

[Route("v{version:apiVersion}/Author/[controller]")]
[ApiVersion("1.0")]
public class AuthorController : MainController
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<GetAuthorViewModel>> Get()
    {
        return _mapper.Map<IEnumerable<GetAuthorViewModel>>(await _authorRepository.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetAuthorViewModel>> Get(Guid id)
    {
        return _mapper.Map<GetAuthorViewModel>(await _authorRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostAuthorViewModel authorViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var author = _mapper.Map<Author>(authorViewModel);

        var validations = new AuthorValidation();
        var result = validations.Validate(author);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _authorRepository.Create(author);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostAuthorViewModel authorViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbAuthor = await _authorRepository.GetById(id);

        if (dbAuthor == null)
        {
            return BadRequest();
        }

        var author = _mapper.Map<Author>(authorViewModel);

        author.Id = id;

        var validations = new AuthorValidation();
        var result = validations.Validate(author);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _authorRepository.Update(author);

        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostAuthorViewModel authorViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbAuthor = await _authorRepository.GetById(id);

        if (dbAuthor == null)
        {
            return BadRequest();
        }

        var author = _mapper.Map<Author>(authorViewModel);

        author.Id = id;

        var validations = new AuthorValidation();
        var result = validations.Validate(author);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _authorRepository.Update(author);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbAuthor = await _authorRepository.GetById(id);

        if (dbAuthor == null)
        {
            return BadRequest();
        }

        await _authorRepository.Delete(id);

        return Ok();
    }
}