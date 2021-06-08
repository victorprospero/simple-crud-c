using FluentValidation;

namespace Inventory.api.Models.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        // possuveis mensagens de validação
        protected readonly string _msdValidSku = "O sku deve ser um número positivo";
        protected readonly string _msdValidName = "O nome deve ser informado";
        protected readonly string _msdValidInventory = "O inventory deve ser informado";
        protected readonly string _msdValidId = "O id deve ser informado";
        protected readonly string _msdValidLocality = "A locality deve ser informada";
        protected readonly string _msdValidQuantity = "A quantity deve ser maior do que zero";
        protected readonly string _msdValidWarehouseType = "O type deve ser 'ECOMMERCE' ou 'PHYSICAL_STORE'";
        protected readonly string _msdValidWarehouses = "Os warehouses devem ser informados";

        /// <summary>
        /// Verifica se um inteiro é positivo
        /// </summary>
        protected static bool PositiveInteger(int x)
        {
            return x > 0;
        }

        /// <summary>
        /// Verifica os tipos válidos do type de um warehouse
        /// </summary>
        protected static bool ValidWarehouseType(string sType)
        {
            return sType == "ECOMMERCE" || sType == "PHYSICAL_STORE";
        }
    }
}
