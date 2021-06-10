namespace Inventory.Middleware.Exceptions
{
    public class ProductNotFoundExeption : ProductException
    {
        public ProductNotFoundExeption(uint sku)
        {
            base.sku = sku;
        }

        public override string Message
        {
            get { return string.Format("O produto com o sku [{0}] não foi encontrado.", base.sku); }
        }
    }
}
