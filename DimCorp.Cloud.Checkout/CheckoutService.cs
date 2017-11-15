using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using DimCorp.Cloud.Checkout.Model;
using DimCorp.Cloud.Common;
using DimCorp.Cloud.ProductCatalog.Model;
using DimCorp.Cloud.UserActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace DimCorp.Cloud.Checkout
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CheckoutService : StatefulService, ICheckoutService
    {
        public CheckoutService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<CheckoutSummary> Checkout(string userId)
        {
            var result = CheckoutSummaryBuilder.Create();

            //call user actor to get the basket
            var userActor = GetUserActor(userId);
            var basket = await userActor.GetBasket();

            //get catalog client
            var catalogService = GetProductCatalogService();

            //constuct CheckoutProduct items by calling to the catalog
            foreach (var basketLine in basket)
            {
                var product = await catalogService.GetProduct(basketLine.Key);
                result.WithProduct(product.ToCheckoutProduct(basketLine.Value));
            }
            
            await userActor.ClearBasket();
            await AddToHistory(result);
            return result;
        }
        
        public async Task<IEnumerable<CheckoutSummary>> GetOrderHitory(string userId)
        {
            var result = new List<CheckoutSummary>();
            var history = await StateManager.GetOrAddAsync<IReliableDictionary<DateTime, CheckoutSummary>>("history");

            using (var tx = StateManager.CreateTransaction())
            {
                var allProducts = await history.CreateEnumerableAsync(tx, EnumerationMode.Unordered);
                using (var enumerator = allProducts.GetAsyncEnumerator())
                    while (await enumerator.MoveNextAsync(CancellationToken.None))
                        result.Add(enumerator.Current.Value);
            }

            return result;
        }

        private async Task AddToHistory(CheckoutSummary checkout)
        {
            var history = await StateManager.GetOrAddAsync<IReliableDictionary<DateTime, CheckoutSummary>>("history");

            using (var tx = StateManager.CreateTransaction())
            {
                await history.AddAsync(tx, checkout.Date, checkout);
                await tx.CommitAsync();
            }
        }

        private IUserActor GetUserActor(string userId)
        {
            return ActorProxy.Create<IUserActor>(new ActorId(userId), ServiceAddress.UserActor);
        }

        private IProductCatalogService GetProductCatalogService()
        {
            return ServiceProxy.Create<IProductCatalogService>(
                ServiceAddress.ProductCatalog,
               new ServicePartitionKey(0));
        }


        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(this.CreateServiceRemotingListener)
            };
        }
    }
}
