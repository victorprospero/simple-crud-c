using System;

namespace Inventory.Middleware.Exceptions
{
    public class ProductException : Exception
    {
        protected uint sku;

        protected ProductException() { }
    }
}
