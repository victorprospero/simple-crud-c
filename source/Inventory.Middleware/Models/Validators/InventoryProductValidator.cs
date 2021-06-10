using FluentValidation;

namespace Inventory.Middleware.Models.Validators
{
    public class InventoryProductValidator : BaseValidator<InventoryProduct>
    {
        public InventoryProductValidator()
        {
            RuleFor(i => i.warehouses)
                .NotNull().WithMessage(base._messageWarehouses);
        }        
    }
}
