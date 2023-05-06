using LeadSoft.Core.Models;

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
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid ArticleId { get; set; }
}