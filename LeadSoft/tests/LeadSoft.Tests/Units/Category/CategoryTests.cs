using LeadSoft.Core.Validations;
using LeadSoft.Tests.Units.Fixtures;

namespace LeadSoft.Tests.Units.Category
{
    [Collection(nameof(CategoryCollection))]
    public class CategoryTests
    {
        private readonly CategoryTestsFixture _categoryTestsFixture;

        public CategoryTests(CategoryTestsFixture categoryTestsFixture)
        {
            _categoryTestsFixture = categoryTestsFixture;
        }

        [Fact(DisplayName = "New Category Valid")]
        [Trait("Category", "Category Tests Fixture")]
        public void Category_NewCategory_MustBeValid()
        {
            // Arrange
            var Category = _categoryTestsFixture.ValidCategory();

            // Act
            var result = new CategoryValidation().Validate(Category);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "New Category Invalid")]
        [Trait("Category", "Category Tests Fixture")]
        public void Category_NewCategory_MustBeInvalid()
        {
            // Arrange
            var Category = _categoryTestsFixture.InvalidCategory();

            // Act
            var result = new CategoryValidation().Validate(Category);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(2, result.Errors.Count);
        }
    }
}
