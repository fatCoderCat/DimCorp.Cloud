using DimCorp.Cloud.ProductCatalog.Model;

namespace DimCorp.Cloud.Checkout.Model
{
    public class CheckoutProduct
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
