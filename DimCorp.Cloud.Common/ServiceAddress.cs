using System;

namespace DimCorp.Cloud.Common
{
    public static class ServiceAddress
    {
        public static readonly Uri UserActor = new Uri("fabric:/DimCorp.Cloud/UserActorService");
        public static readonly Uri ProductCatalog = new Uri("fabric:/DimCorp.Cloud/DimCorp.Cloud.ProductCatalog");
        public static readonly Uri Checkout = new Uri("fabric:/DimCorp.Cloud/DimCorp.Cloud.Checkout");
    }
}
