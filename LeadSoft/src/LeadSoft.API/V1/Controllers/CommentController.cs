using AutoMapper;
using LeadSoft.API.Controllers;
using LeadSoft.API.ViewModels;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.V1.Controllers;

[Route("v{version:apiVersion}/Comment/[controller]")]
[ApiVersion("1.0")]
public class CommentController : MainController
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentController(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<GetCommentViewModel>> Get()
    {
        return _mapper.Map<IEnumerable<GetCommentViewModel>>(await _commentRepository.Get());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetCommentViewModel>> Get(Guid id)
    {
        return _mapper.Map<GetCommentViewModel>(await _commentRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostCommentViewModel commentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var article = _mapper.Map<Comment>(commentViewModel);

        var validations = new CommentValidation();
        var result = validations.Validate(article);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _commentRepository.Create(article);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostCommentViewModel commentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbComment = await _commentRepository.GetById(id);

        if (dbComment == null)
        {
            return BadRequest();
        }

        var comment = _mapper.Map<Comment>(commentViewModel);

        comment.Id = id;

        var validations = new CommentValidation();
        var result = validations.Validate(comment);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _commentRepository.Update(comment);

        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostCommentViewModel commentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbComment = await _commentRepository.GetById(id);

        if (dbComment == null)
        {
            return BadRequest();
        }

        var comment = _mapper.Map<Comment>(commentViewModel);

        comment.Id = id;

        var validations = new CommentValidation();
        var result = validations.Validate(comment);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        await _commentRepository.Update(comment);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var dbComment = await _commentRepository.GetById(id);

        if (dbComment == null)
        {
            return BadRequest();
        }

        await _commentRepository.Delete(id);

        return Ok();
    }
}