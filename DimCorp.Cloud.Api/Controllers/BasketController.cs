using System;
using System.Linq;
using System.Threading.Tasks;
using DimCorp.Cloud.Api.Model;
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
            var actor = GetActor(userId);
            var products = await actor.GetBasket();
            return new ApiBasket
            {
                UserId = userId,
                Items = products.Select(x =>
                    new ApiBasketItem
                    {
                        ProductId = x.Key.ToString(),
                        Quantity = x.Value
                    })
                    .ToArray()
            };
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
            return ActorProxy.Create<IUserActor>(
                new ActorId(userId),
                new Uri("fabric:/DimCorp.Cloud/UserActorService"));
        }
    }
}