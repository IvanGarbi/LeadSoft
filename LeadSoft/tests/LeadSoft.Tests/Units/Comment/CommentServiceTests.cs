using LeadSoft.Core.Notifications;
using LeadSoft.Core.Services;
using LeadSoft.Data.Context;
using LeadSoft.Data.Repository;
using LeadSoft.Tests.Units.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LeadSoft.Tests.Units.Comment
{
    [Collection(nameof(CommentCollection))]
    public class CommentServiceTests
    {
        private readonly CommentTestsFixture _commentTestsFixture;

        public CommentServiceTests(CommentTestsFixture articleTestsFixture)
        {
            _commentTestsFixture = articleTestsFixture;
        }


        [Fact(DisplayName = "New Comment With Success")]
        [Trait("Comment", "Comment Service Tests")]
        public async void CommentService_NewComment_ExecuteWithSuccess()
        {
            //Arrange  
            var comment = _commentTestsFixture.ValidComment();
            comment.ArticleId = comment.Article.Id;
            comment.Article.AuthorId = comment.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb19")
                .Options;

            var notify = new Mock<Notify>();


            CommentRepository commentRepository = new CommentRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(comment.Article.Author);
            }

            comment.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment.Article);
            }

            comment.Article = null;

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment);
            }

            var result = await commentRepository.Get();
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Empty(listNotification);
            Assert.True(isEmptyNotification);
        }

        [Fact(DisplayName = "New Comment Without Success")]
        [Trait("Comment", "Comment Service Tests")]
        public async void CommentService_NewComment_ExecuteWithoutSuccess()
        {
            //Arrange  
            var comment = _commentTestsFixture.InvalidComment();
            comment.ArticleId = comment.Article.Id;
            comment.Article.AuthorId = comment.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb20")
                .Options;

            var notify = new Mock<Notify>();


            CommentRepository commentRepository = new CommentRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(comment.Article.Author);
            }

            comment.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment.Article);
            }

            comment.Article = null;

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment);
            }

            var result = await commentRepository.Get();
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            //Assert  
            Assert.Empty(result);
            Assert.NotEmpty(listNotification);
            Assert.Equal(1, listNotification.Count);
            Assert.False(isEmptyNotification);
        }


        [Fact(DisplayName = "Update Comment With Success")]
        [Trait("Comment", "Comment Service Tests")]
        public async void CommentService_Update_ExecuteWithSuccess()
        {
            //Arrange  
            var comment = _commentTestsFixture.ValidComment();
            comment.ArticleId = comment.Article.Id;
            comment.Article.AuthorId = comment.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb21")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CommentRepository commentRepository = new CommentRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(comment.Article.Author);
            }

            comment.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment.Article);
            }

            comment.Article = null;

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment);
            }

            var result = await commentRepository.GetById(comment.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var commentActual = comment;
            commentActual.Text = "text text text";

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Update(commentActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultActual = await commentRepository.GetById(commentActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultActual);
            Assert.Equal("text text text", resultActual.Text);
            Assert.Empty(listNotification);
            Assert.Empty(listNotificationActual);
            Assert.True(isEmptyNotification);
            Assert.True(isEmptyNotificationActual);
        }

        [Fact(DisplayName = "Update Comment Withou Success")]
        [Trait("Comment", "Comment Service Tests")]
        public async void CommentService_Update_ExecuteWithoutSuccess()
        {
            //Arrange  
            var comment = _commentTestsFixture.ValidComment();
            comment.ArticleId = comment.Article.Id;
            comment.Article.AuthorId = comment.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb22")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CommentRepository commentRepository = new CommentRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(comment.Article.Author);
            }

            comment.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment.Article);
            }

            comment.Article = null;

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment);
            }

            var result = await commentRepository.GetById(comment.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var commentActual = comment;
            commentActual.Text = string.Empty;

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Update(commentActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultAtual = await commentRepository.GetById(commentActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultAtual);
            Assert.NotEqual(string.Empty, resultAtual.Text);
            Assert.Empty(listNotification);
            Assert.NotEmpty(listNotificationActual);
            Assert.Equal(1, listNotificationActual.Count);
            Assert.True(isEmptyNotification);
            Assert.False(isEmptyNotificationActual);
        }


        [Fact(DisplayName = "Delete Comment With Success")]
        [Trait("Comment", "Comment Service Tests")]
        public async void CommentService_Delete_ExecuteWithSuccess()
        {
            //Arrange  
            var comment = _commentTestsFixture.ValidComment();
            comment.ArticleId = comment.Article.Id;
            comment.Article.AuthorId = comment.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb23")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CommentRepository authorRepository = new CommentRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(comment.Article.Author);
            }

            comment.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment.Article);
            }

            comment.Article = null;

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment);
            }

            var result = await authorRepository.GetById(comment.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
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

        [Fact(DisplayName = "Delete Comment Without Success")]
        [Trait("Comment", "Comment Service Tests")]
        public async void CommentService_Delete_ExecuteWithoutSuccess()
        {
            //Arrange  
            var comment = _commentTestsFixture.ValidComment();
            comment.ArticleId = comment.Article.Id;
            comment.Article.AuthorId = comment.Article.Author.Id;

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb24")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            CommentRepository authorRepository = new CommentRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(comment.Article.Author);
            }

            comment.Article.Author = null;

            using (var context = new ArticleService(new ArticleRepository(new LeadSoftDbContext(options)), notify.Object, new AuthorRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment.Article);
            }

            comment.Article = null;

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notify.Object, new ArticleRepository(new LeadSoftDbContext(options))))
            {
                await context.Create(comment);
            }

            var result = await authorRepository.GetById(comment.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new CommentService(new CommentRepository(new LeadSoftDbContext(options)), notifyActual.Object, new ArticleRepository(new LeadSoftDbContext(options))))
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
