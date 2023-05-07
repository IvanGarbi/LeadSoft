using LeadSoft.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace LeadSoft.API.ViewModels;

public class GetCategoryViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Guid ArticleId { get; set; }
    public Article Article { get; set; }
}

public class PostCategoryViewModel
{
    [Required(ErrorMessage = "The field {0} is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public Guid ArticleId { get; set; }
}