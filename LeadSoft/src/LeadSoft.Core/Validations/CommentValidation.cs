using FluentValidation;
using LeadSoft.Core.Models;

namespace LeadSoft.Core.Validations
{
    public class CommentValidation : AbstractValidator<Comment>
    {
        public CommentValidation()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");
        }
    }
}
