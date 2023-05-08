
namespace LeadSoft.Tests.Units.Fixtures
{
    [CollectionDefinition(nameof(CategoryCollection))]
    public class CategoryCollection : ICollectionFixture<CategoryTestsFixture>
    { }

    public class CategoryTestsFixture : IDisposable
    {
        public LeadSoft.Core.Models.Category ValidCategory()
        {
            var category = new LeadSoft.Core.Models.Category
            {
                Id = Guid.NewGuid(),
                ArticleId = Guid.NewGuid(),
                Name = "Nome qualquer",
                Type = "Tipo qualquer",
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

            return category;
        }

        public LeadSoft.Core.Models.Category InvalidCategory()
        {
            var category = new LeadSoft.Core.Models.Category
            {
                Id = Guid.NewGuid(),
                ArticleId = Guid.NewGuid(),
                Name = "",
                Type = "",
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

            return category;
        }

        public void Dispose()
        {
        }
    }
}
