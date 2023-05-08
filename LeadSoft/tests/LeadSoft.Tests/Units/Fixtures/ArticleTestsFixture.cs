
namespace LeadSoft.Tests.Units.Fixtures
{
    [CollectionDefinition(nameof(ArticleCollection))]
    public class ArticleCollection : ICollectionFixture<ArticleTestsFixture>
    { }

    public class ArticleTestsFixture : IDisposable
    {
        public LeadSoft.Core.Models.Article ValidArticle()
        {
            var article = new LeadSoft.Core.Models.Article
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
            };

            return article;
        }

        public LeadSoft.Core.Models.Article InvalidArticle()
        {
            var article = new LeadSoft.Core.Models.Article
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.NewGuid(),
                Text = "",
                Description = "",
                Author = new Core.Models.Author
                {
                    Id = Guid.NewGuid(),
                    DateOfBirth = DateTime.Today.AddYears(-20),
                    Email = "pedro@gmail.com",
                    FirstName = "Pedro",
                    LastName = "Silva"
                }
            };

            return article;
        }

        public void Dispose()
        {
        }
    }
}
