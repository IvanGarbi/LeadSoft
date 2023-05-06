using LeadSoft.Core.Models;

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
    public string Title { get; set; }
    public string Description { get; set; }
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
}