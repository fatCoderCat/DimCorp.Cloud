using DimCorp.Cloud.Checkout.Model;
using DimCorp.Cloud.ProductCatalog.Model;

namespace DimCorp.Cloud.Checkout
{
    public static class Converter
    {
        public static CheckoutProduct ToCheckoutProduct(this Product product, int quantity)
        {
            return new CheckoutProduct
            {
                Product = product,
                Price = product.Price,
                Quantity = quantity
            };
        }
    }
}
