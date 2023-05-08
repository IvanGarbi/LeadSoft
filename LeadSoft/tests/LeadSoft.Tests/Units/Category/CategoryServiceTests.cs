using LeadSoft.Core.Notifications;
using LeadSoft.Core.Services;
using LeadSoft.Data.Context;
using LeadSoft.Data.Repository;
using LeadSoft.Tests.Units.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LeadSoft.Tests.Units.Category
{
    [Collection(nameof(CategoryCollection))]
    public class CategoryServiceTests
    {
        private readonly CategoryTestsFixture _categoryTestsFixture;

        public CategoryServiceTests(CategoryTestsFixture articleTestsFixture)
        {
            _categoryTestsFixture = articleTestsFixture;
        }


        [Fact(DisplayName = "New Category With Success")]
        [Trait("Category", "Category Service Tests")]
        public async void CategoryService_NewCategory_ExecuteWithSuccess()
        {
            //Arrange  
            var category = _categoryTestsFixture.ValidCategory();
            category.ArticleId = category.Article.Id;
            category.Article.AuthorId = category.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb13")
                .Options;

            var notify = new Mock<Notify>();


            CategoryRepository categoryRepository = new CategoryRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(category.Article.Author);
            }

            category.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category.Article);
            }

            category.Article = null;

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category);
            }

            var result = await categoryRepository.Get();
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Empty(listNotification);
            Assert.True(isEmptyNotification);
        }

        [Fact(DisplayName = "New Category Without Success")]
        [Trait("Category", "Category Service Tests")]
        public async void CategoryService_NewCategory_ExecuteWithoutSuccess()
        {
            //Arrange  
            var category = _categoryTestsFixture.InvalidCategory();
            category.ArticleId = category.Article.Id;
            category.Article.AuthorId = category.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb14")
                .Options;

            var notify = new Mock<Notify>();


            CategoryRepository categoryRepository = new CategoryRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(category.Article.Author);
            }

            category.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category.Article);
            }

            category.Article = null;

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category);
            }

            var result = await categoryRepository.Get();
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            //Assert  
            Assert.Empty(result);
            Assert.NotEmpty(listNotification);
            Assert.Equal(2, listNotification.Count);
            Assert.False(isEmptyNotification);
        }


        [Fact(DisplayName = "Update Category With Success")]
        [Trait("Category", "Category Service Tests")]
        public async void CategoryService_Update_ExecuteWithSuccess()
        {
            //Arrange  
            var category = _categoryTestsFixture.ValidCategory();
            category.ArticleId = category.Article.Id;
            category.Article.AuthorId = category.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb15")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CategoryRepository categoryRepository = new CategoryRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(category.Article.Author);
            }

            category.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category.Article);
            }

            category.Article = null;

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category);
            }

            var result = await categoryRepository.GetById(category.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var categoryActual = category;
            categoryActual.Name = "name name name";
            categoryActual.Type = "type type type";

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Update(categoryActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultActual = await categoryRepository.GetById(categoryActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultActual);
            Assert.Equal("name name name", resultActual.Name);
            Assert.Equal("type type type", resultActual.Type);
            Assert.Empty(listNotification);
            Assert.Empty(listNotificationActual);
            Assert.True(isEmptyNotification);
            Assert.True(isEmptyNotificationActual);
        }

        [Fact(DisplayName = "Update Category Withou Success")]
        [Trait("Category", "Category Service Tests")]
        public async void CategoryService_Update_ExecuteWithoutSuccess()
        {
            //Arrange  
            var category = _categoryTestsFixture.ValidCategory();
            category.ArticleId = category.Article.Id;
            category.Article.AuthorId = category.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb16")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CategoryRepository categoryRepository = new CategoryRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(category.Article.Author);
            }

            category.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category.Article);
            }

            category.Article = null;

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category);
            }

            var result = await categoryRepository.GetById(category.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var categoryActual = category;
            categoryActual.Name = string.Empty;
            categoryActual.Type = string.Empty;

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Update(categoryActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultAtual = await categoryRepository.GetById(categoryActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultAtual);
            Assert.NotEqual(string.Empty, resultAtual.Name);
            Assert.NotEqual(string.Empty, resultAtual.Type);
            Assert.Empty(listNotification);
            Assert.NotEmpty(listNotificationActual);
            Assert.Equal(2, listNotificationActual.Count);
            Assert.True(isEmptyNotification);
            Assert.False(isEmptyNotificationActual);
        }


        [Fact(DisplayName = "Delete Category With Success")]
        [Trait("Category", "Category Service Tests")]
        public async void CategoryService_Delete_ExecuteWithSuccess()
        {
            //Arrange  
            var category = _categoryTestsFixture.ValidCategory();
            category.ArticleId = category.Article.Id;
            category.Article.AuthorId = category.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb17")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CategoryRepository authorRepository = new CategoryRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(category.Article.Author);
            }

            category.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category.Article);
            }

            category.Article = null;

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category);
            }

            var result = await authorRepository.GetById(category.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Delete(result.Id);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultActual = await authorRepository.Get();
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.Empty(resultActual);
            Assert.Empty(listNotification);
            Assert.Empty(listNotificationActual);
            Assert.True(isEmptyNotification);
            Assert.True(isEmptyNotificationActual);
        }

        [Fact(DisplayName = "Delete Category Without Success")]
        [Trait("Category", "Category Service Tests")]
        public async void CategoryService_Delete_ExecuteWithoutSuccess()
        {
            //Arrange  
            var category = _categoryTestsFixture.ValidCategory();
            category.ArticleId = category.Article.Id;
            category.Article.AuthorId = category.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb18")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CategoryRepository authorRepository = new CategoryRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(category.Article.Author);
            }

            category.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category.Article);
            }

            category.Article = null;

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(category);
            }

            var result = await authorRepository.GetById(category.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new CategoryService(new CategoryRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Delete(Guid.NewGuid());
            }

            var listaNotificacoesAtualizado = notifyActual.Object.GetNotifications();
            var resultActual = await authorRepository.Get();
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotEmpty(resultActual);
            Assert.Empty(listNotification);
            Assert.NotEmpty(listaNotificacoesAtualizado);
            Assert.Equal(1, listaNotificacoesAtualizado.Count);
            Assert.True(isEmptyNotification);
            Assert.False(isEmptyNotificationActual);
        }
    }
}
