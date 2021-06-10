using Inventory.Middleware.Exceptions;
using Inventory.Middleware.Models;
using Inventory.Middleware.Services.Implementation;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Inventory.Tests
{
    public class Service_Product_Tests
    {
        [Theory(DisplayName = "Create Product - Ok")]
        [Trait("Services", "ProductService")]
        [InlineData(10, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 15, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_STORE")]
        [InlineData(20, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "ECOMMERCE")]
        public void ProductController_Create_Ok(uint sku, string name, string local1, uint qtty1, string type1, string local2, uint qtty2, string type2)
        {
            // Arrange
            ProductService service = new ProductService();
            Product product = new Product
            {
                sku = sku,
                name = name,
                inventory = new InventoryProduct
                {
                    warehouses = new List<Warehouse>
                    {
                        new Warehouse { locality = local1, quantity = qtty1, type = type1 },
                        new Warehouse { locality = local2, quantity = qtty2, type = type2 }
                    }
                }
            };
            bool bOk = true;
            // Act
            try 
            {
                service.Create(product);
            } 
            catch
            {
                bOk = false;
            }
            // Assert
            Assert.True(bOk);
        }

        [Theory(DisplayName = "Create Product - Not Valid")]
        [Trait("Services", "ProductService")]
        [InlineData(10, null, "SP", 0, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_S")]
        [InlineData(20, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "NOT_VALID")]
        public void ProductController_Create_NotValid(uint sku, string name, string local1, uint qtty1, string type1, string local2, uint qtty2, string type2)
        {
            // Arrange
            ProductService service = new ProductService();
            Product product = new Product
            {
                sku = sku,
                name = name,
                inventory = new InventoryProduct
                {
                    warehouses = new List<Warehouse>
                    {
                        new Warehouse { locality = local1, quantity = qtty1, type = type1 },
                        new Warehouse { locality = local2, quantity = qtty2, type = type2 }
                    }
                }
            };

            // Act & Assert
            Assert.Throws<ProductNotValidException>(() => service.Create(product));
        }

        [Theory(DisplayName = "Update Product - Not Exists")]
        [Trait("Services", "ProductService")]
        [InlineData(2846, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_STORE")]
        [InlineData(3215, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_STORE")]
        public void ProductService_Update_NotExists(uint sku, string name, string local1, uint qtty1, string type1, string local2, uint qtty2, string type2)
        {
            // Arrange
            ProductService service = new ProductService();
            Product product = new Product
            {
                sku = sku,
                name = name,
                inventory = new InventoryProduct
                {
                    warehouses = new List<Warehouse>
                    {
                        new Warehouse { locality = local1, quantity = qtty1, type = type1 },
                        new Warehouse { locality = local2, quantity = qtty2, type = type2 }
                    }
                }
            };

            // Act & Assert
            Assert.Throws<ProductNotFoundExeption>(() => service.Update(product));
        }

        [Theory(DisplayName = "Retrieve Product - Not Exists")]
        [Trait("Services", "ProductService")]
        [InlineData(1234)]
        [InlineData(5678)]
        public void ProductService_Retrieve_NotExists(uint sku)
        {
            // Arrange
            ProductService service = new ProductService();

            // Act & Assert
            Assert.Throws<ProductNotFoundExeption>(() => service.Retrieve(sku));
        }

        [Theory(DisplayName = "Delete Product - Not Exists")]
        [Trait("Services", "ProductService")]
        [InlineData(1234)]
        [InlineData(5678)]
        public void ProductService_Delete_NotExists(uint sku)
        {
            // Arrange
            ProductService service = new ProductService();

            // Act & Assert
            Assert.Throws<ProductNotFoundExeption>(() => service.Delete(sku));
        }
    }
}
