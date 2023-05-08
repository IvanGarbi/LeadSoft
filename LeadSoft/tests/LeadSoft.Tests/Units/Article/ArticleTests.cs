using LeadSoft.Core.Validations;
using LeadSoft.Tests.Units.Fixtures;

namespace LeadSoft.Tests.Units.Article
{
    [Collection(nameof(ArticleCollection))]
    public class ArticleTests
    {
        private readonly ArticleTestsFixture _articleTestsFixture;

        public ArticleTests(ArticleTestsFixture articleTestsFixture)
        {
            _articleTestsFixture = articleTestsFixture;
        }

        [Fact(DisplayName = "New Article Valid")]
        [Trait("Category", "Article Tests Fixture")]
        public void Article_NewArticle_MustBeValid()
        {
            // Arrange
            var Article = _articleTestsFixture.ValidArticle();

            // Act
            var result = new ArticleValidation().Validate(Article);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "New Article Invalid")]
        [Trait("Category", "Article Tests Fixture")]
        public void Article_NewArticle_MustBeInvalid()
        {
            // Arrange
            var Article = _articleTestsFixture.InvalidArticle();

            // Act
            var result = new ArticleValidation().Validate(Article);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(4, result.Errors.Count);
        }
    }
}
