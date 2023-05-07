using LeadSoft.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace LeadSoft.API.ViewModels;

public class GetArticleViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    public Category Category { get; set; }
    public ICollection<Comment> Comments { get; set; }
}

public class PostArticleViewModel
{
    [Required(ErrorMessage = "The field {0} is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public string Text { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public Guid AuthorId { get; set; }
}