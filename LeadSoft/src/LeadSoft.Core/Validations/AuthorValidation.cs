using FluentValidation;
using LeadSoft.Core.Models;

namespace LeadSoft.Core.Validations
{
    public class AuthorValidation : AbstractValidator<Author>
    {
        public AuthorValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");
            
            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");
            
            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.")
                .EmailAddress()
                .WithMessage("The {PropertyName} is an invalid e-mail.");
        }
    }
}
