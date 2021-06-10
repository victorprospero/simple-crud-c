using Inventory.Middleware.Models;
using Inventory.Middleware.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Middleware.Repositories.Implementation
{
    public class ProductRepository : BaseRepository<Product>, IRepository<Product>
    {
        public void Create(Product model)
        {
            base.GetPersistedList().Add(model);
        }

        public void Delete(uint sku)
        {
            int itemIndex = base.GetPersistedList().FindIndex(p => p.sku == sku);
            if (itemIndex > -1) base.GetPersistedList().RemoveAt(itemIndex);
        }

        public Product Retrieve(uint sku)
        {
            return base.GetPersistedList().FirstOrDefault(p => p.sku == sku);
        }

        public IEnumerable<Product> Retrieve()
        {
            return base.GetPersistedList();
        }

        public void Update(Product model)
        {
            int itemIndex = base.GetPersistedList().FindIndex(p => p.sku == model.sku);
            if (itemIndex > -1) base.GetPersistedList()[itemIndex] = model;
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
