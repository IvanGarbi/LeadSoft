using LeadSoft.Core.Models;

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
    public string Name { get; set; }
    public string Type { get; set; }
    public Guid ArticleId { get; set; }
}