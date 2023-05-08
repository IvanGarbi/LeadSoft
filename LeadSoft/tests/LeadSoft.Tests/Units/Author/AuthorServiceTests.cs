using LeadSoft.Core.Notifications;
using LeadSoft.Core.Services;
using LeadSoft.Data.Context;
using LeadSoft.Data.Repository;
using LeadSoft.Tests.Units.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LeadSoft.Tests.Units.Author
{
    [Collection(nameof(AuthorCollection))]
    public class AuthorServiceTests
    {
        private readonly AuthorTestsFixture _authorTestsFixture;

        public AuthorServiceTests(AuthorTestsFixture AuthorTestsFixture)
        {
            _authorTestsFixture = AuthorTestsFixture;
        }


        [Fact(DisplayName = "New Author With Success")]
        [Trait("Category", "Author Service Tests")]
        public async void AuthorService_NewAuthor_ExecuteWithSuccess()
        {
            //Arrange  
            var author = _authorTestsFixture.ValidAuthor();
            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb1")
                .Options;

            var notify = new Mock<Notify>();


            AuthorRepository authorRepository = new AuthorRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(author);
            }
            var result = await authorRepository.Get();
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();
            
            //Assert  
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Empty(listNotification);
            Assert.True(isEmptyNotification);
        }

        [Fact(DisplayName = "New Author Without Success")]
        [Trait("Category", "Author Service Tests")]
        public async void AuthorService_NewAuthor_ExecuteWithoutSuccess()
        {
            //Arrange  
            var author = _authorTestsFixture.InvalidAuthor();
            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb2")
                .Options;

            var notify = new Mock<Notify>();


            AuthorRepository authorRepository = new AuthorRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(author);
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


        [Fact(DisplayName = "Update Author With Success")]
        [Trait("Category", "Author Service Tests")]
        public async void AuthorService_Update_ExecuteWithSuccess()
        {
            //Arrange  
            var author = _authorTestsFixture.ValidAuthor();

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb3")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            AuthorRepository authorRepository = new AuthorRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(author);
            }
            var result = await authorRepository.GetById(author.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var authorActual = author;
            authorActual.FirstName = "Pedro";
            authorActual.Email = "pedro@gmail.com";
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notifyActual.Object))
            {
                await context.Update(authorActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultActual = await authorRepository.GetById(authorActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultActual);
            Assert.Equal("Pedro", resultActual.FirstName);
            Assert.Equal("pedro@gmail.com", resultActual.Email);
            Assert.Empty(listNotification);
            Assert.Empty(listNotificationActual);
            Assert.True(isEmptyNotification);
            Assert.True(isEmptyNotificationActual);
        }

        [Fact(DisplayName = "Update Author Withou Success")]
        [Trait("Category", "Author Service Tests")]
        public async void AuthorService_Update_ExecuteWithoutSuccess()
        {
            //Arrange  
            var author = _authorTestsFixture.ValidAuthor();

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb4")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            AuthorRepository authorRepository = new AuthorRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(author);
            }
            var result = await authorRepository.GetById(author.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            var authorActual = author;
            authorActual.FirstName = "";
            authorActual.Email = "";
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notifyActual.Object))
            {
                await context.Update(authorActual);
            }

            var listNotificationActual = notifyActual.Object.GetNotifications();
            var resultAtual = await authorRepository.GetById(authorActual.Id);
            var isEmptyNotificationActual = notifyActual.Object.IsEmptyNotification();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultAtual);
            Assert.NotEqual(string.Empty, resultAtual.FirstName);
            Assert.Empty(listNotification);
            Assert.NotEmpty(listNotificationActual);
            Assert.Equal(3, listNotificationActual.Count);
            Assert.True(isEmptyNotification);
            Assert.False(isEmptyNotificationActual);
        }


        [Fact(DisplayName = "Delete Author With Success")]
        [Trait("Category", "Author Service Tests")]
        public async void AuthorService_Delete_ExecuteWithSuccess()
        {
            //Arrange  
            var author = _authorTestsFixture.ValidAuthor();

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb5")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            AuthorRepository authorRepository = new AuthorRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(author);
            }
            var result = await authorRepository.GetById(author.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notifyActual.Object))
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

        [Fact(DisplayName = "Delete Author Without Success")]
        [Trait("Category", "Author Service Tests")]
        public async void AuthorService_Delete_ExecuteWithoutSuccess()
        {
            //Arrange  
            var author = _authorTestsFixture.ValidAuthor();

            var options = new DbContextOptionsBuilder<LeadSoftDbContext>()
                .UseInMemoryDatabase("LeadSoftCaseDb6")
                .Options;

            var notify = new Mock<Notify>();
            var notifyActual = new Mock<Notify>();

            AuthorRepository authorRepository = new AuthorRepository(new LeadSoftDbContext(options));

            // Act
            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notify.Object))
            {
                await context.Create(author);
            }
            var result = await authorRepository.GetById(author.Id);
            var listNotification = notify.Object.GetNotifications();
            var isEmptyNotification = notify.Object.IsEmptyNotification();

            using (var context = new AuthorService(new AuthorRepository(new LeadSoftDbContext(options)), notifyActual.Object))
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
