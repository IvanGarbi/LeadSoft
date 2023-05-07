using LeadSoft.Core.Interfaces.Notifications;
using LeadSoft.Core.Interfaces.Repository;
using LeadSoft.Core.Interfaces.Services;
using LeadSoft.Core.Models;
using LeadSoft.Core.Validations;

namespace LeadSoft.Core.Services;

public class AuthorService : MainService, IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository, INotify notify) : base(notify)
    {
        _authorRepository = authorRepository;
    }

    public async Task Create(Author author)
    {
        var validation = Validate(new AuthorValidation(), author);

        if (!validation)
        {
            return;
        }

        var authorDb = await _authorRepository.Get();

        if (authorDb.Where(x => x.Email == author.Email).Any())
        {
            Notify("This e-mail is already used.");

            return;
        }

        await _authorRepository.Create(author);
    }

    public async Task Update(Author author)
    {
        var validation = Validate(new AuthorValidation(), author);

        if (!validation)
        {
            return;
        }

        var dbAuthor = await _authorRepository.GetById(author.Id);

        if (dbAuthor == null)
        {
            Notify("This author does not exists.");

            return;
        }

        await _authorRepository.Update(author);
    }

    public async Task Delete(Guid id)
    {
        var dbAuthor = await _authorRepository.GetById(id);

        if (dbAuthor == null)
        {
            Notify("This author does not exists.");

            return;
        }

        await _authorRepository.Delete(id);
    }

    public async void Dispose()
    {
        _authorRepository?.Dispose();
    }
}