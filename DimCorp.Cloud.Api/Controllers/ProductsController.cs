using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimCorp.Cloud.Api.Model;
using DimCorp.Cloud.Common;
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
                ServiceAddress.ProductCatalog,
                new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<ApiProduct>> Get()
        {
            var allProducts = await _catalogService.GetAllProducts();
            return allProducts.Select(p => p.ToVm());
        }

        [HttpGet("{productId}")]
        public async Task<ApiProduct> Get(Guid productId)
        {
            var product = await _catalogService.GetProduct(productId);
            return product.ToVm();
        }

        [HttpPost]
        public async Task Post([FromBody] ApiProduct product)
        {
            var newProduct = product.ToModel().WithNewGuid().WithAvailability(100);

            await _catalogService.AddProduct(newProduct);
        }
    }
}
