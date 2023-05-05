
namespace LeadSoft.Core.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
