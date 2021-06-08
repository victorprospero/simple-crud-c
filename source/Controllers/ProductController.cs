using Inventory.api.Data;
using Inventory.api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Atributos e Construtor

        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Criação de produto onde o payload será o json informado (exceto as propriedades isMarketable e inventory.quantity).
        /// </summary>
        /// <remarks>
        /// Dispara um BadRequest se o payload estiver em um formato não esperado ou se o produto já existir.
        /// </remarks>
        /// <param name="newProduct">Definição do payload</param>
        [HttpPost("Create")]
        public async Task<ActionResult> Create(Product newProduct)
        {
            // valida a definição do produto
            if (newProduct == null) return BadRequest();

            // valida se o produto existe e está preenchido corretamente
            if (await this.ProductExists(newProduct.sku)) return BadRequest();
            newProduct.Validate(); // Pode disparar exception na validação

            // salva o produto e retorna OK 
            await UpdateDependencies(newProduct);
            await this._dataContext.Products.AddAsync(newProduct);
            await this._dataContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Edição de produto por sku.
        /// </summary>
        /// <param name="editingProduct">Produto sendo editado.</param>
        /// <remarks>Dispara um NotFound se o sku do produto não existir e dispara um BadRequest se o payload não estiver no formato esperado.</remarks>
        [HttpPut("Update")]
        public async Task<ActionResult> Update(Product editingProduct)
        {
            // valida a definição do produto
            if (editingProduct == null) return BadRequest();
            
            // valida se o produto existe e se está preenchido corretamente
            if (!await this.ProductExists(editingProduct.sku)) return NotFound();
            editingProduct.Validate(); // Pode disparar exception na validação

            // salva o produto e retorna OK 
            await UpdateDependencies(editingProduct);
            this._dataContext.Products.Update(editingProduct);
            await this._dataContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Recuperação de produto por sku.
        /// </summary>
        /// <param name="sku">Sku do produto a ser recuperado.</param>
        /// <remarks>Dispara um NotFound se o produto não existir.</remarks>
        [HttpGet("Retrieve")]
        public async Task<ActionResult<Product>> Retrieve(int sku)
        {
            // busca o produto pelo sku
            var product = await this._dataContext.Products.FindAsync(sku);
            // valida se o produto existe
            if (product == null) return NotFound(); 
            // inclui o inventory
            var inventory = await this._dataContext.Inventories.FindAsync(sku);
            product.inventory = inventory;
            // inclui os warehouses
            if (inventory != null)
            {
                inventory.warehouses = new List<Warehouse>();
                inventory.warehouses.AddRange(this._dataContext.Warehouses.Where(w => w.sku == sku).ToArray());
            }
            // retorna o produto desejado
            return product;
        }

        /// <summary>
        /// Deleção de produto por sku.
        /// </summary>
        /// <param name="sku">Sku do produto a ser removido.</param>
        /// <remarks>Dispara um NotFound se o produto não existir</remarks>
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int sku)
        {
            // busca o produto a ser removido
            var product = await this._dataContext.Products.FindAsync(sku);
            
            // valida se o produto existe
            if (product == null) return NotFound();

            // remove o produto
            await RemoveDependencies(sku);
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Verifica se um produto existe
        /// </summary>
        private async Task<bool> ProductExists(int sku)
        {
            var product = await this._dataContext.Products.FindAsync(sku);
            return product != null;
        }

        /// <summary>
        /// Remove os inventories e warehouses de um produto
        /// </summary>
        /// <param name="sku">Sku do produto</param>
        private async Task RemoveDependencies(int sku)
        {
            // remove o inventory (um para um)
            var inventory = await this._dataContext.Inventories.FindAsync(sku);
            if (inventory != null) _dataContext.Inventories.Remove(inventory);

            // remove os warehouses
            var warehouses = this._dataContext.Warehouses.Where(w => w.sku == sku).ToArray();
            if (warehouses != null)
                foreach (var w in warehouses)
                    this._dataContext.Warehouses.Remove(w);
        }

        /// <summary>
        /// Atualiza os inventories e warehouses de um produto
        /// </summary>
        private async Task UpdateDependencies(Product p)
        {
            // remove as dependencias antigas
            await RemoveDependencies(p.sku);
            // se existe inventory, inclui ele
            if (p.inventory != null)
            {
                await this._dataContext.Inventories.AddAsync(p.inventory);
                // se existe(m) warehouse(s), inclui elas
                if (p.inventory.warehouses != null)
                    foreach (Warehouse w in p.inventory.warehouses)
                        await this._dataContext.Warehouses.AddAsync(w);
            }
        }

        #endregion
    }
}