using LeadSoft.Core.Interfaces.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainController : ControllerBase
{
    protected readonly INotify _notify;

    public MainController(INotify notify)
    {
        _notify = notify;
    }

    protected ActionResult Respose(object result = null)
    {
        if (_notify.IsEmptyNotification())
        {
            return Ok(new
            {
                sucesso = true,
                data = result
            });
        }

        return BadRequest(new
        {
            sucesso = false,
            errors = _notify.GetNotifications().Select(n => n.Message)
        });
    }
}