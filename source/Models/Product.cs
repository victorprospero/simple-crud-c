using FluentValidation.Results;
using Inventory.api.Models.Validators;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Inventory.api.Models
{
    /// <summary>
    /// Modelagem de um produto
    /// </summary>
    public class Product : BaseModel
    {
        //Chave-primaria da entidade Product
        [Key]
        public int sku { get; set; }

        public string name { get; set; }

        public InventoryProduct inventory { get; set; }

        public bool isMarketable { get { return inventory == null ? false : inventory.quantity > 0; } }

        /// <summary>
        /// Valida se o produto está pronto para ser salvo
        /// </summary>
        public void Validate()
        {
            List<ValidationFailure> validationErrors = new List<ValidationFailure>();
            validationErrors.AddRange(new ProductValidator().Validate(this).Errors);
            if (this.inventory != null)
            {
                this.inventory.sku = this.sku;
                validationErrors.AddRange(new InventoryProductValidator().Validate(this.inventory).Errors);
                if (this.inventory.warehouses != null)
                    foreach (Warehouse w in this.inventory.warehouses)
                    {
                        w.sku = this.sku;
                        validationErrors.AddRange(new WarehouseValidator().Validate(w).Errors);
                    }
            }
            List<string> errorMessages = new List<string>();
            foreach (ValidationFailure error in validationErrors)
                errorMessages.Add(error.ErrorMessage);
            if (errorMessages.Count > 0) throw new System.Exception(string.Concat("Validation errors:\n", string.Join("\n", errorMessages.ToArray())));
        }
    }
}