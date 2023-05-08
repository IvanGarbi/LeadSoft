using LeadSoft.Core.Notifications;
using LeadSoft.Core.Services;
using LeadSoft.Data.Context;
using LeadSoft.Data.Repository;
using LeadSoft.Tests.Units.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LeadSoft.Tests.Units.Article
{
    [Collection(nameof(ArticleCollection))]
    public class ArticleServiceTests
    {
        private readonly ArticleTestsFixture _articleTestsFixture;

        public ArticleServiceTests(ArticleTestsFixture articleTestsFixture)
        {
            _articleTestsFixture = articleTestsFixture;
        }


        [Fact(DisplayName = "New Article With Success")]
        [Trait("Category", "Article Service Tests")]
        public async void ArticleService_NewArticle_ExecuteWithSuccess()
        {
            //Arrange  
            var article = _articleTestsFixture.ValidArticle();
            article.AuthorId = article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb7")
                .Options;

            var notify = new Mock<Notify>();


            ArticleRepository articleRepository = new ArticleRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(article.Author);
            }
            
            article.Author = null;
            
            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(article);
            }

            var result = await articleRepository.Get();
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Empty(listNotification);
            Assert.True(isEmptyNotification);
        }

        [Fact(DisplayName = "New Article Without Success")]
        [Trait("Category", "Article Service Tests")]
        public async void ArticleService_NewArticle_ExecuteWithoutSuccess()
        {
            //Arrange  
            var article = _articleTestsFixture.InvalidArticle();
            article.AuthorId = article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb8")
                .Options;

            var notify = new Mock<Notify>();


            ArticleRepository authorRepository = new ArticleRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(article.Author);
            }

            article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(article);
            }
            var result = await authorRepository.Get();
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            //Assert  
            Assert.Empty(result);
            Assert.NotEmpty(listNotification);
            Assert.Equal(4, listNotification.Count);
            Assert.False(isEmptyNotification);
        }


        [Fact(DisplayName = "Update Article With Success")]
        [Trait("Category", "Article Service Tests")]
        public async void ArticleService_Update_ExecuteWithSuccess()
        {
            //Arrange  
            var article = _articleTestsFixture.ValidArticle();
            article.AuthorId = article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb9")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            ArticleRepository authorRepository = new ArticleRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(article.Author);
            }

            article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(article);
            }

            var result = await authorRepository.GetById(article.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var authorActual = article;
            authorActual.Text = "Textinho";
            authorActual.Title = "Titulo titulo";

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notifyActual.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Update(authorActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultActual = await authorRepository.GetById(authorActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultActual);
            Assert.Equal("Textinho", resultActual.Text);
            Assert.Equal("Titulo titulo", resultActual.Title);
            Assert.Empty(listNotification);
            Assert.Empty(listNotificationActual);
            Assert.True(isEmptyNotification);
            Assert.True(isEmptyNotificationActual);
        }

        [Fact(DisplayName = "Update Article Withou Success")]
        [Trait("Category", "Article Service Tests")]
        public async void ArticleService_Update_ExecuteWithoutSuccess()
        {
            //Arrange  
            var article = _articleTestsFixture.ValidArticle();
            article.AuthorId = article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb10")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            ArticleRepository authorRepository = new ArticleRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(article.Author);
            }

            article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(article);
            }

            var result = await authorRepository.GetById(article.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var authorActual = article;
            authorActual.Text = string.Empty;
            authorActual.Title = string.Empty;
            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notifyActual.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Update(authorActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultAtual = await authorRepository.GetById(authorActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultAtual);
            Assert.NotEqual(string.Empty, resultAtual.Title);
            Assert.NotEqual(string.Empty, resultAtual.Text);
            Assert.Empty(listNotification);
            Assert.NotEmpty(listNotificationActual);
            Assert.Equal(2, listNotificationActual.Count);
            Assert.True(isEmptyNotification);
            Assert.False(isEmptyNotificationActual);
        }


        [Fact(DisplayName = "Delete Article With Success")]
        [Trait("Category", "Article Service Tests")]
        public async void ArticleService_Delete_ExecuteWithSuccess()
        {
            //Arrange  
            var article = _articleTestsFixture.ValidArticle();
            article.AuthorId = article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb11")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            ArticleRepository authorRepository = new ArticleRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(article.Author);
            }

            article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(article);
            }

            var result = await authorRepository.GetById(article.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notifyActual.Object, new AuthorRepository(new LeadSoftDbContext(options))))
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

        [Fact(DisplayName = "Delete Article Without Success")]
        [Trait("Category", "Article Service Tests")]
        public async void ArticleService_Delete_ExecuteWithoutSuccess()
        {
            //Arrange  
            var article = _articleTestsFixture.ValidArticle();
            article.AuthorId = article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb12")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            ArticleRepository authorRepository = new ArticleRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(article.Author);
            }

            article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(article);
            }

            var result = await authorRepository.GetById(article.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notifyActual.Object, new AuthorRepository(new LeadSoftDbContext(options))))
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
