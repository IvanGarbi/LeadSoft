using LeadSoft.Core.Validations;
using LeadSoft.Tests.Units.Fixtures;

namespace LeadSoft.Tests.Units.Comment
{
    [Collection(nameof(CommentCollection))]
    public class CommentTests
    {
        private readonly CommentTestsFixture _commentTestsFixture;

        public CommentTests(CommentTestsFixture commentTestsFixture)
        {
            _commentTestsFixture = commentTestsFixture;
        }

        [Fact(DisplayName = "New Comment Valid")]
        [Trait("Category", "Comment Tests Fixture")]
        public void Comment_NewComment_MustBeValid()
        {
            // Arrange
            var comment = _commentTestsFixture.ValidComment();

            // Act
            var result = new CommentValidation().Validate(comment);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "New Comment Invalid")]
        [Trait("Category", "Comment Tests Fixture")]
        public void Comment_NewComment_MustBeInvalid()
        {
            // Arrange
            var comment = _commentTestsFixture.InvalidComment();

            // Act
            var result = new CommentValidation().Validate(comment);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(1, result.Errors.Count);
        }
    }
}
