
namespace LeadSoft.Tests.Units.Fixtures
{
    [CollectionDefinition(nameof(CommentCollection))]
    public class CommentCollection : ICollectionFixture<CommentTestsFixture>
    { }

    public class CommentTestsFixture : IDisposable
    {
        public LeadSoft.Core.Models.Comment ValidComment()
        {
            var comment = new LeadSoft.Core.Models.Comment
            {
                Id = Guid.NewGuid(),
                Text = "Comentário legal",
                ArticleId = Guid.NewGuid(),
                Article = new Core.Models.Article
                {
                    Id = Guid.NewGuid(),
                    AuthorId = Guid.NewGuid(),
                    Text = "Texto qualquer",
                    Description = "Descrição",
                    Title = "Título qualquer",
                    Author = new Core.Models.Author
                    {
                        Id = Guid.NewGuid(),
                        DateOfBirth = DateTime.Today.AddYears(-20),
                        Email = "pedro@gmail.com",
                        FirstName = "Pedro",
                        LastName = "Silva"
                    }
                }
            };

            return comment;
        }

        public LeadSoft.Core.Models.Comment InvalidComment()
        {
            var comment = new LeadSoft.Core.Models.Comment
            {
                Id = Guid.NewGuid(),
                Text = "",
                ArticleId = Guid.NewGuid(),
                Article = new Core.Models.Article
                {
                    Id = Guid.NewGuid(),
                    AuthorId = Guid.NewGuid(),
                    Text = "Texto qualquer",
                    Description = "Descrição",
                    Title = "Título qualquer",
                    Author = new Core.Models.Author
                    {
                        Id = Guid.NewGuid(),
                        DateOfBirth = DateTime.Today.AddYears(-20),
                        Email = "pedro@gmail.com",
                        FirstName = "Pedro",
                        LastName = "Silva"
                    }
                }
            };

            return comment;
        }

        public void Dispose()
        {
        }
    }
}
