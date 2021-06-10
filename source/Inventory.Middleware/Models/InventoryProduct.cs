using System.Collections.Generic;
using System.Linq;

namespace Inventory.Middleware.Models
{
    public class InventoryProduct : BaseModel
    {
        public long quantity { get { return warehouses == null ? 0 : warehouses.Sum(t => t.quantity); } }
        public IEnumerable<Warehouse> warehouses { get; set; }
    }
}
