using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.api.Models
{
    [JsonObject("warehouses")]
    public class Warehouse : BaseModel
    {
        public Warehouse()
        {
            this.id = Guid.NewGuid();
        }

        /// <summary>
        /// O id é a chave primaria, seu valor é gerado automaticamente no construtor.
        /// </summary>
        [JsonIgnore]
        [Key]
        public Guid id { get; set; }

        /// <summary>
        /// Sku é a chave estrangeira para o InventoryProduct
        /// </summary>
        [JsonIgnore]
        public int sku { get; set; }

        public string locality { get; set; }

        public int quantity { get; set; }

        public string type { get; set; }
    }
}
