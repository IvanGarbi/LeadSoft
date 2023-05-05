
namespace LeadSoft.Core.Models
{
    public class Comment : Entity
    {
        public string Text { get; set; }

        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
