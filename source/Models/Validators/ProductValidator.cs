using FluentValidation;

namespace Inventory.api.Models.Validators
{
    public class ProductValidator : BaseValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.sku)
                .Must(PositiveInteger).WithMessage(base._msdValidSku);

            RuleFor(p => p.name)
                .NotNull().WithMessage(_msdValidName)
                .NotEmpty().WithMessage(_msdValidName);

            RuleFor(p => p.inventory)
                .NotNull().WithMessage(_msdValidInventory);
        }

    }
}
