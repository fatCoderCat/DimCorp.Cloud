using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimCorp.Cloud.Api.Model;
using DimCorp.Cloud.Checkout.Model;
using DimCorp.Cloud.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace DimCorp.Cloud.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : Controller
    {
        private static readonly Random Rnd = new Random(DateTime.UtcNow.Second);

        [Route("{userId}")]
        public async Task<ApiCheckoutSummary> Checkout(string userId)
        {
            var summary = await GetCheckoutService().Checkout(userId);
            return summary.ToVm();
        }

        [Route("history/{userId}")]
        public async Task<IEnumerable<ApiCheckoutSummary>> GetHistory(string userId)
        {
            var history = await GetCheckoutService().GetOrderHitory(userId);
            return history.Select(x => x.ToVm());
        }

        private ICheckoutService GetCheckoutService()
        {
            var key = LongRandom();

            return ServiceProxy.Create<ICheckoutService>(
                ServiceAddress.Checkout,
                new ServicePartitionKey(key));
        }

        private long LongRandom()
        {
            byte[] buf = new byte[8];
            Rnd.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return longRand;
        }
    }
}
