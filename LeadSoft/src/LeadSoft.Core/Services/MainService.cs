using FluentValidation;
using LeadSoft.Core.Interfaces.Notifications;
using LeadSoft.Core.Models;
using LeadSoft.Core.Notifications;

namespace LeadSoft.Core.Services;

public abstract class MainService
{
    private readonly INotify _notify;

    protected MainService(INotify notify)
    {
        _notify = notify;
    }

    protected bool Validate<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid)
        {
            return true;
        }

        foreach (var error in validator.Errors)
        {
            _notify.AddNotification(new Notification(error.ErrorMessage));
        }

        return false;
    }

    protected void Notify(string mensagem)
    {
        _notify.AddNotification(new Notification(mensagem));
    }
}