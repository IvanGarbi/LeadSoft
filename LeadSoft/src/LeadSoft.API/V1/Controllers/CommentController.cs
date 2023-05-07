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

[Route("v{version:apiVersion}/Comment/[controller]")]
[ApiVersion("1.0")]
public class CommentController : MainController
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentRepository commentRepository, IMapper mapper, ICommentService commentService, INotify notify) : base(notify)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IEnumerable<GetCommentViewModel>> Get()
    {
        var comments = await _commentRepository.Get();
        var commentsViewModels = _mapper.Map<IEnumerable<GetCommentViewModel>>(comments);
        return commentsViewModels;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetCommentViewModel>> Get(Guid id)
    {
        var comment = await _commentRepository.GetById(id);

        if (comment == null)
        {
            _notify.AddNotification(new Notification("This comment does not exists."));
            
            return Respose();
        }

        return _mapper.Map<GetCommentViewModel>(comment);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostCommentViewModel commentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var article = _mapper.Map<Comment>(commentViewModel);

        await _commentService.Create(article);

        return Respose();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PostCommentViewModel commentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var comment = _mapper.Map<Comment>(commentViewModel);

        comment.Id = id;

        await _commentService.Update(comment);

        return Respose();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PostCommentViewModel commentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        var comment = _mapper.Map<Comment>(commentViewModel);

        comment.Id = id;

        await _commentService.Update(comment);

        return Respose();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { ModelState });
        }

        await _commentService.Delete(id);

        return Respose();
    }
}