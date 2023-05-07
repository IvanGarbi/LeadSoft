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

[Route("v{version:apiVersion}/Author/[controller]")]
[ApiVersion("1.0")]
public class AuthorController : MainController
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IAuthorService _authorService;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorRepository authorRepository, IMapper mapper, IAuthorService authorService, INotify notify) : base(notify)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IEnumerable<GetAuthorViewModel>> Get()
    {
        var authors = await _authorRepository.Get();
        var authorViewModels = _mapper.Map<IEnumerable<GetAuthorViewModel>>(authors);
        return authorViewModels;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetAuthorViewModel>> Get(Guid id)
    {
        var author = await _authorRepository.GetById(id);

        if (author == null)
        {
            _notify.AddNotification(new Notification("This author does not exists."));
            return Respose();
        }

        return _mapper.Map<GetAuthorViewModel>(author);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostAuthorViewModel authorViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var author = _mapper.Map<Author>(authorViewModel);

        await _authorService.Create(author);

        return Respose();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostAuthorViewModel authorViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var dbAuthor = await _authorRepository.GetById(id);

        if (dbAuthor == null)
        {
            return BadRequest();
        }

        var author = _mapper.Map<Author>(authorViewModel);

        author.Id = id;

        await _authorService.Update(author);

        return Respose();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostAuthorViewModel authorViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var dbAuthor = await _authorRepository.GetById(id);

        if (dbAuthor == null)
        {
            return BadRequest();
        }

        var author = _mapper.Map<Author>(authorViewModel);

        author.Id = id;

        await _authorService.Update(author);

        return Respose();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        await _authorService.Delete(id);

        return Respose();
    }
}