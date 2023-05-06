using LeadSoft.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LeadSoft.API.V1.Controllers;

[Route("v{version:apiVersion}/Category/[controller]")]
[ApiVersion("1.0")]
public class CategoryController : MainController
{
    
}