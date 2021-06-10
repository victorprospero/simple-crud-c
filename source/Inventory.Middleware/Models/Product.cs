namespace Inventory.Middleware.Models
{
    public class Product : BaseModel
    {
        private InventoryProduct _inventory = new InventoryProduct();

        public uint sku { get; set; }
        public string name { get; set; }
        public InventoryProduct inventory { get; set; }
        public bool isMarketable { get { return inventory == null ? false : inventory.quantity > 0; } }
    }
}
