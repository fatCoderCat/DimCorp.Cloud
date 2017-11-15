using System;
using System.Threading.Tasks;
using DimCorp.Cloud.ProductCatalog.Model;

namespace DimCorp.Cloud.ProductCatalog
{
    internal static class ProductRepositorySeeder
    {
        public static async Task SeedInitialData(IProductRepository repository)
        {
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dell Monitor",
                Description = "Computer Monitor",
                Price = 500,
                Availability = 100
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Surface Book",
                Description = "Microsoft's Laptop",
                Price = 2200,
                Availability = 15
            };

            var product3 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Arc Touch Mouse",
                Description = "Computer Mouse",
                Price = 60,
                Availability = 30
            };

            await repository.AddProduct(product1);
            await repository.AddProduct(product2);
            await repository.AddProduct(product3);
        }
    }
}
