namespace Inventory.Middleware.Models
{
    public class Warehouse : BaseModel
    {
        public string locality { get; set; }
        public uint quantity { get; set; }
        public string type { get; set; }
    }
}
