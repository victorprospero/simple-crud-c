using FluentValidation;
using System;

namespace Inventory.api.Models.Validators
{
    public class WarehouseValidator : BaseValidator<Warehouse>
    {
        public WarehouseValidator()
        {
            RuleFor(w => w.id)
                .NotEqual(Guid.Empty).WithMessage(_msdValidId);
            RuleFor(w => w.locality)
                .NotEmpty().WithMessage(_msdValidLocality)
                .NotNull().WithMessage(_msdValidLocality);
            RuleFor(w => w.quantity)
                .Must(PositiveInteger).WithMessage(_msdValidQuantity);
            RuleFor(w => w.sku)
                .Must(PositiveInteger).WithMessage(_msdValidSku);
            RuleFor(w => w.type)
                .Must(ValidWarehouseType).WithMessage(_msdValidWarehouseType);
        }
    }
}
