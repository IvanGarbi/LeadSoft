using FluentValidation;
using LeadSoft.Core.Models;

namespace LeadSoft.Core.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .NotNull()
                .WithMessage("The {PropertyName} must be informed.");
        }
    }
}
