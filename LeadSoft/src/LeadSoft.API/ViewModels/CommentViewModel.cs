using LeadSoft.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace LeadSoft.API.ViewModels;

public class GetCommentViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid ArticleId { get; set; }
    public Article Article { get; set; }
}

public class PostCommentViewModel
{
    [Required(ErrorMessage = "The field {0} is required.")]
    public string Text { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public Guid ArticleId { get; set; }
}