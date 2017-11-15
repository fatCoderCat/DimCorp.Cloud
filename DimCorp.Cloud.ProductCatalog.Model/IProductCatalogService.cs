using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace DimCorp.Cloud.ProductCatalog.Model
{
    public interface IProductCatalogService : IService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task AddProduct(Product product);
    }
}
