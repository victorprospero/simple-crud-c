﻿using Inventory.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryProduct> Inventories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
   
    }
}
