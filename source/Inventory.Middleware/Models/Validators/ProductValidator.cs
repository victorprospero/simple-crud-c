using FluentValidation;

namespace Inventory.Middleware.Models.Validators
{
    public class ProductValidator : BaseValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.sku)
                .Must(IsGreaterThanZero).WithMessage(base._messageSku);

            RuleFor(p => p.name)
                .NotNull().WithMessage(_messageName)
                .NotEmpty().WithMessage(_messageName);

            RuleFor(p => p.inventory)
                .NotNull().WithMessage(_messageInventory);

        }

    }
}
