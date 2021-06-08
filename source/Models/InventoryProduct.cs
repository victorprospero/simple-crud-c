using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Inventory.api.Models
{
    [JsonObject("inventory")]
    public class InventoryProduct : BaseModel
    {
        /// <summary>
        /// InventoryProduct possui uma relação 1 para 1 com a entidade Product,
        /// por isso, foi criado o campo sku que é chave-primária e também é
        /// chave estrangeira para esta entidade
        /// </summary>
        [JsonIgnore]
        [Key]
        public int sku { get; set; }

        public int quantity { get { return warehouses == null ? 0 : warehouses.Sum(t => t.quantity); } }

        public List<Warehouse> warehouses { get; set; }
    }
}