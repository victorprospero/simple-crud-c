using FluentValidation;
using System;

namespace Inventory.Middleware.Models.Validators
{
    public class WarehouseValidator : BaseValidator<Warehouse>
    {
        public WarehouseValidator()
        {
            RuleFor(w => w.locality)
                .NotEmpty().WithMessage(_messageLocality)
                .NotNull().WithMessage(_messageLocality);
            RuleFor(w => w.quantity)
                .Must(IsGreaterThanZero).WithMessage(_messageQuantity);
            RuleFor(w => w.type)
                .Must(IsValidWarehouseType).WithMessage(_messageWarehouseType);
        }
    }
}
