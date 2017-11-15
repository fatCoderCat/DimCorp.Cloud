using System.Threading.Tasks;
using DimCorp.Cloud.Api.Model;
using DimCorp.Cloud.Common;
using DimCorp.Cloud.UserActor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace DimCorp.Cloud.Api.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        [HttpGet("{userId}")]
        public async Task<ApiBasket> Get(string userId)
        {
            var products = await GetActor(userId).GetBasket();
            return products.ToVm(userId);
        }

        [HttpPost("{userId}")]
        public async Task Add(string userId, [FromBody] ApiBasketAddRequest request)
        {
            var actor = GetActor(userId);
            await actor.AddToBasket(request.ProductId, request.Quantity);
        }

        [HttpDelete("{userId}")]
        public async Task Delete(string userId)
        {
            var actor = GetActor(userId);
            await actor.ClearBasket();
        }

        private IUserActor GetActor(string userId)
        {
            return ActorProxy.Create<IUserActor>(new ActorId(userId), ServiceAddress.UserActor);
        }
    }
}