using System;

namespace DimCorp.Cloud.ProductCatalog.Model
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Availability { get; set; }
    }

    public static class ProductBuilder
    {
        public static Product WithNewGuid(this Product product)
        {
            product.Id = Guid.NewGuid();
            return product;
        }

        public static Product WithAvailability(this Product product, int quantity)
        {
            product.Availability = quantity;
            return product;
        }
    }
}
