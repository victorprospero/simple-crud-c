using FluentValidation;

namespace Inventory.Middleware.Models.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        protected readonly string _messageSku = "O sku deve ser um número maior do que zero";
        protected readonly string _messageName = "O nome deve ser informado";
        protected readonly string _messageInventory = "O inventory precisa ser informado";
        protected readonly string _messageWarehouses= "Os warehouses precisam ser informados";
        protected readonly string _messageLocality = "A locality deve ser informada";
        protected readonly string _messageQuantity = "A quantity deve ser maior do que zero";
        protected readonly string _messageWarehouseType = "O type deve ser 'ECOMMERCE' ou 'PHYSICAL_STORE'";
        

        protected static bool IsGreaterThanZero(uint x)
        {
            return x > 0;
        }

        protected static bool IsValidWarehouseType(string sType)
        {
            return sType == "ECOMMERCE" || sType == "PHYSICAL_STORE";
        }
    }
}
