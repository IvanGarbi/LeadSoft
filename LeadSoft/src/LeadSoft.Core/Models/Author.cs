
namespace LeadSoft.Core.Models
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
