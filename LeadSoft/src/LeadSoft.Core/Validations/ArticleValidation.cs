using FluentValidation;
using LeadSoft.Core.Models;

namespace LeadSoft.Core.Validations
{
    public class ArticleValidation : AbstractValidator<Article>
    {
        public ArticleValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");

            RuleFor(x => x.Text)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");
        }
    }
}
