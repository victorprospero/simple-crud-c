using Inventory.api.Models;
using Xunit;

namespace Inventory.tests
{
    public class ProductModel_Test
    {

        #region Testes

        [Theory(DisplayName = "Quantidade do inventory válida")]
        [Trait("Categoria", "Model Product")]
        [InlineData(new int[] { 1, 1 }, 2)]
        [InlineData(new int[] { 1, 3, 5 }, 9)]
        [InlineData(null, null)]
        public void Product_InventoryQuantity_IsValid(int[] warehouseQuantities, int? expectedResult)
        {
            // Arrange/ Act
            Product p = Product_BuildWarehouseQuantities(warehouseQuantities);

            // Assert

            //se não informou as quantidades, o resultado e o inventory devem ser nulos
            if (warehouseQuantities == null)
            {
                Assert.Null(expectedResult);
                Assert.Null(p.inventory);
            }
            //se informou, verifica se a somatoria confere com a esperada
            else
                Assert.Equal(expected: expectedResult, actual: p.inventory.quantity);
        }

        [Theory(DisplayName = "Quantidade do inventory inválida")]
        [Trait("Categoria", "Model Product")]
        [InlineData(new int[] { 1, 1 }, 4)]
        [InlineData(new int[] { 1, 3, 5 }, 2)]
        [InlineData(null, 5)]
        public void Product_InventoryQuantity_IsInvalid(int[] warehouseQuantities, int? expectedResult)
        {
            // Arrange/ Act
            Product p = Product_BuildWarehouseQuantities(warehouseQuantities);

            // Assert

            //se não informou as quantidades, o resultado não deve ser nulo
            if (warehouseQuantities == null)
            {
                Assert.NotNull(expectedResult);
            }
            //se informou, verifica se a somatoria confere com a esperada
            else
                Assert.NotEqual(expected: expectedResult, actual: p.inventory.quantity);
        }

        [Theory(DisplayName = "Verifica se o produto pode ser vendido")]
        [Trait("Categoria", "Model Product")]
        [InlineData(new int[] { 1, 1 })]
        [InlineData(new int[] { 1, 3, 5 })]
        public void Product_isMarketable_True(int[] warehouseQuantities)
        {
            // Arrange/ Act
            Product p = Product_BuildWarehouseQuantities(warehouseQuantities);

            // Assert
            Assert.True(p.isMarketable);
        }

        [Theory(DisplayName = "Verifica se o produto não pode ser vendido")]
        [Trait("Categoria", "Model Product")]
        [InlineData(new int[] { 0, 0 })]
        [InlineData(null)]
        public void Product_isMarketable_False(int[] warehouseQuantities)
        {
            // Arrange/ Act
            Product p = Product_BuildWarehouseQuantities(warehouseQuantities);

            // Assert
            Assert.False(p.isMarketable);
        }

        [Theory(DisplayName ="Verifica se o produto está preenchido corretamente")]
        [Trait("Categoria", "Model Product")]
        [InlineData(43264, "L'Oréal Professionnel Expert Absolut Repair Cortex Lipidium - Máscara de Reconstrução 500g", "SP", 12, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_STORE")]
        [InlineData(10000, "Segundo Produto", "SP", 8, "ECOMMERCE", "MOEMA", 1, "PHYSICAL_STORE")]
        public void Product_Fields_isValid(int sku, string name, string local1, int qtty1, string type1, string local2, int qtty2, string type2)
        {
            // Arrange
            Product p = new Product
            {
                sku = sku,
                name = name,
                inventory = new InventoryProduct
                {
                    warehouses = new System.Collections.Generic.List<Warehouse>()
                }
            };
            p.inventory.warehouses.AddRange(new Warehouse[] { 
                new Warehouse { locality = local1, quantity = qtty1, type = type1 },
                new Warehouse { locality = local2, quantity = qtty2, type = type2 } 
            });

            // Act
            bool bIsValid = true;
            try { p.Validate(); }
            catch { bIsValid = false; }

            // Assert
            Assert.True(bIsValid);
        }

        [Theory(DisplayName = "Verifica se o produto está preenchido incorretamente")]
        [Trait("Categoria", "Model Product")]
        [InlineData(43264, null, "SP", 12, "ECOMMERCE", "MOEMA", 3, "PHYSICAL_STORE")]
        [InlineData(10000, "Segundo Produto", "SP", 8, "ECOMMERCE", "MOEMA", 1, "SEM_DOMINIO")]
        public void Product_Fields_isInvalid(int sku, string name, string local1, int qtty1, string type1, string local2, int qtty2, string type2)
        {
            // Arrange
            Product p = new Product
            {
                sku = sku,
                name = name,
                inventory = new InventoryProduct
                {
                    warehouses = new System.Collections.Generic.List<Warehouse>()
                }
            };
            p.inventory.warehouses.AddRange(new Warehouse[] {
                new Warehouse { locality = local1, quantity = qtty1, type = type1 },
                new Warehouse { locality = local2, quantity = qtty2, type = type2 }
            });

            // Act
            bool bIsValid = true;
            try { p.Validate(); }
            catch { bIsValid = false; }

            // Assert
            Assert.False(bIsValid);
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Monta um produto com inventory e warehouses com quantidades informadas
        /// </summary>
        private Product Product_BuildWarehouseQuantities(int[] warehouseQuantities)
        {
            Product p = new Product();

            // se informou as quantidades, cria o inventory e seu container de warehouses
            if (warehouseQuantities != null)
            {
                p.inventory = new InventoryProduct();
                p.inventory.warehouses = new System.Collections.Generic.List<Warehouse>();
                // inclui um warehouse para cada uma das quantidades informadas
                foreach (int qtty in warehouseQuantities)
                {
                    Warehouse w = new Warehouse();
                    w.quantity = qtty;
                    p.inventory.warehouses.Add(w);
                }
            }
            return p;
        }

        #endregion
    }
}
