using LeadSoft.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.V1.Controllers;

[Route("v{version:apiVersion}/Comment/[controller]")]
[ApiVersion("1.0")]
public class CommentController : MainController
{
    
}