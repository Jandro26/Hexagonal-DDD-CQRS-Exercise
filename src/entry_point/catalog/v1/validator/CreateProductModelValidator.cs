using FluentValidation;
using Hexagonal_Exercise.entry_point.catalog.v1.model;

namespace Hexagonal_Exercise.entry_point.catalog.v1.validator
{
    public class CreateProductModelValidator: AbstractValidator<CreateProductModel>
    {
        public CreateProductModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(1, 99999)
                .WithMessage("The product Id must not be empty nor null and between 1 and 99999.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .WithMessage("The name must not be empty nor null and no longer than 20 characters.");
        }
    }
}
