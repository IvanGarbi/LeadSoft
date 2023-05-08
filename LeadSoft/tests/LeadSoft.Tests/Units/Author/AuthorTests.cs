using LeadSoft.Core.Validations;
using LeadSoft.Tests.Units.Fixtures;

namespace LeadSoft.Tests.Units.Author
{
    [Collection(nameof(AuthorCollection))]
    public class AuthorTests
    {
        private readonly AuthorTestsFixture _authorTestsFixture;

        public AuthorTests(AuthorTestsFixture authorTestsFixture)
        {
            _authorTestsFixture = authorTestsFixture;
        }

        [Fact(DisplayName = "New Author Valid")]
        [Trait("Category", "Author Tests Fixture")]
        public void Author_NewAuthor_MustBeValid()
        {
            // Arrange
            var Author = _authorTestsFixture.ValidAuthor();

            // Act
            var result = new AuthorValidation().Validate(Author);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "New Author Invalid")]
        [Trait("Category", "Author Tests Fixture")]
        public void Author_NewAuthor_MustBeInvalid()
        {
            // Arrange
            var Author = _authorTestsFixture.InvalidAuthor();

            // Act
            var result = new AuthorValidation().Validate(Author);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(4, result.Errors.Count);
        }
    }
}
