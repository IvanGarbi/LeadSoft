
namespace LeadSoft.Tests.Units.Fixtures
{
    [CollectionDefinition(nameof(AuthorCollection))]
    public class AuthorCollection : ICollectionFixture<AuthorTestsFixture>
    { }

    public class AuthorTestsFixture : IDisposable
    {
        public LeadSoft.Core.Models.Author ValidAuthor()
        {
            var author = new LeadSoft.Core.Models.Author
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Today.AddYears(- 20),
                Email = "pedro@gmail.com",
                FirstName = "Pedro",
                LastName = "Silva"
            };

            return author;
        }

        public LeadSoft.Core.Models.Author InvalidAuthor()
        {
            var author = new LeadSoft.Core.Models.Author
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Today.AddYears(-20),
                Email = "",
                FirstName = "",
                LastName = ""
            };

            return author;
        }

        public void Dispose()
        {
        }
    }
}
