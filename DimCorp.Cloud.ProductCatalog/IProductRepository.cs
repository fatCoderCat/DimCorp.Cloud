using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DimCorp.Cloud.ProductCatalog.Model;

namespace DimCorp.Cloud.ProductCatalog
{
    interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task AddProduct(Product product);

        Task<Product> GetProduct(Guid productId);
    }
}
