using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimCorp.Cloud.Api.Model;
using DimCorp.Cloud.ProductCatalog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace DimCorp.Cloud.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductCatalogService _catalogService;

        public ProductsController()
        {
            _catalogService = ServiceProxy.Create<IProductCatalogService>(
                new Uri("fabric:/DimCorp.Cloud/DimCorp.Cloud.ProductCatalog"),
                new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<ApiProduct>> Get()
        {
            IEnumerable<Product> allProducts = await _catalogService.GetAllProducts();

            return allProducts.Select(p => new ApiProduct
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsAvailable = p.Availability > 0
            });
        }

        [HttpGet("{productId}")]
        public async Task<ApiProduct> Get(Guid productId)
        {
            var product = await _catalogService.GetProduct(productId);

            //TODO: make converter
            return new ApiProduct
            {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    IsAvailable = product.Availability > 0
            };
        }

        [HttpPost]
        public async Task Post([FromBody] ApiProduct product)
        {
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Availability = 100
            };

            await _catalogService.AddProduct(newProduct);
        }
    }
}
