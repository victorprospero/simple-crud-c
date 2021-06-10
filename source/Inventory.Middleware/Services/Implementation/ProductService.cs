using FluentValidation.Results;
using Inventory.Middleware.Exceptions;
using Inventory.Middleware.Models;
using Inventory.Middleware.Models.Validators;
using Inventory.Middleware.Repositories.Implementation;
using Inventory.Middleware.Services.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Middleware.Services.Implementation
{
    public class ProductService : BaseService<ProductRepository>, IService<Product>
    {
        public void Create(Product product)
        {
            this.Validate(product);
            if (base.GetRepository().Retrieve(product.sku) != null) throw new ProductAlreadyExistsExeption(product.sku);
            base.GetRepository().Create(product);
        }

        public IEnumerable<Product> Retrieve()
        {
            return base.GetRepository().Retrieve();
        }

        public Product Retrieve(uint sku)
        {
            Product p = base.GetRepository().Retrieve(sku);
            if (p == null) throw new ProductNotFoundExeption(sku);
            return p;
        }

        public void Update(Product product)
        {
            this.Validate(product);
            if (base.GetRepository().Retrieve(product.sku) == null) throw new ProductNotFoundExeption(product.sku);
            base.GetRepository().Update(product);
        }

        public void Delete(uint sku)
        {
            if (base.GetRepository().Retrieve(sku) == null) throw new ProductNotFoundExeption(sku);
            base.GetRepository().Delete(sku);
        }

        private void Validate(Product product)
        {
            List<ValidationResult> validations = new List<ValidationResult> {
                new ProductValidator().Validate(product)
            };
            if (product.inventory != null)
            {
                validations.Add(new InventoryProductValidator().Validate(product.inventory));
                if (product.inventory.warehouses != null)
                    foreach (Warehouse warehouse in product.inventory.warehouses)
                        validations.Add(new WarehouseValidator().Validate(warehouse));
            }
            if (validations.FirstOrDefault(x => !x.IsValid) != null)
                throw new ProductNotValidException(product.sku, validations);
        }
    }
}
