using Inventory.api.Controllers;
using Inventory.api.Data;
using Inventory.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Inventory.tests
{
    public class ProductController_Test
    {
        [Theory(DisplayName = "Criação de um novo produto com erro")]
        [Trait("Categoria", "Controller Product")]
        [InlineData(0, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_STORE")]
        [InlineData(43264, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "NOT_VALID")]
        public async void ProductController_Create_NotOk(int sku, string name, string local1, int qtty1, string type1, string local2, int qtty2, string type2)
        {
            // Arrange
            Product p = new Product
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

            // Act
            bool bIsValid = true;
            try
            {
                var dbContext = new Mock<DataContext>();
                ActionResult r = await new ProductController(dbContext.Object).Create(p);
                if (r.GetType() != typeof(OkResult)) bIsValid = false;
            }
            catch { bIsValid = false; }

            // Assert
            Assert.False(bIsValid);
        }

        [Theory(DisplayName = "Atualização de um produto com erro")]
        [Trait("Categoria", "Controller Product")]
        [InlineData(0, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_STORE")]
        [InlineData(43264, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "NOT_VALID")]
        public async void ProductController_Update_NotOk(int sku, string name, string local1, int qtty1, string type1, string local2, int qtty2, string type2)
        {
            // Arrange
            Product p = new Product
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

            // Act
            bool bIsValid = true;
            try
            {
                var dbContext = new Mock<DataContext>();
                ActionResult r = await new ProductController(dbContext.Object).Update(p);
                if (r.GetType() != typeof(OkResult)) bIsValid = false;
            }
            catch { bIsValid = false; }

            // Assert
            Assert.False(bIsValid);
        }

        [Theory(DisplayName = "Busca por um produto com erro")]
        [Trait("Categoria", "Controller Product")]
        [InlineData(10)]
        [InlineData(43264)]
        public async void ProductController_Retrieve_NotOk(int sku)
        {
            // Arrange /  Act
            bool bIsValid = true;
            try
            {
                var dbContext = new Mock<DataContext>();
                ActionResult<Product> p = await new ProductController(dbContext.Object).Retrieve(sku);
                if (p ==  null) bIsValid = false;
            }
            catch { bIsValid = false; }

            // Assert
            Assert.False(bIsValid);
        }

        [Theory(DisplayName = "Exclusão de um produto com erro")]
        [Trait("Categoria", "Controller Product")]
        [InlineData(10)]
        [InlineData(43264)]
        public async void ProductController_Delete_NotOk(int sku)
        {
            // Arrange /  Act
            bool bIsValid = true;
            try
            {
                var dbContext = new Mock<DataContext>();
                ActionResult p = await new ProductController(dbContext.Object).Delete(sku);
                if (p == null) bIsValid = false;
            }
            catch { bIsValid = false; }

            // Assert
            Assert.False(bIsValid);
        }
    }
}
