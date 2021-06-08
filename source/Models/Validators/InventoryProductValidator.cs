using FluentValidation;

namespace Inventory.api.Models.Validators
{
    public class InventoryProductValidator : BaseValidator<InventoryProduct>
    {
        public InventoryProductValidator()
        {
            RuleFor(x => x.sku)
                .Must(PositiveInteger).WithMessage(base._msdValidSku);
            RuleFor(x => x.warehouses)
                .NotNull().WithMessage(_msdValidWarehouses);
        }
    }
}
