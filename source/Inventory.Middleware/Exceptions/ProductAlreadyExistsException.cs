namespace Inventory.Middleware.Exceptions
{
    public class ProductAlreadyExistsExeption : ProductException
    {
        public ProductAlreadyExistsExeption(uint sku)
        {
            base.sku = sku;
        }

        public override string Message
        {
            get { return string.Format("O produto com o sku [{0}] já existe.", base.sku); }
        }
    }
}
