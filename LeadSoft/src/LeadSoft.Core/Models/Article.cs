
namespace LeadSoft.Core.Models
{
    public class Article : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
