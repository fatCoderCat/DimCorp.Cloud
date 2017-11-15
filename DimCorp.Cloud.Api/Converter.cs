using System;
using System.Collections.Generic;
using System.Linq;
using DimCorp.Cloud.Api.Model;
using DimCorp.Cloud.Checkout.Model;
using DimCorp.Cloud.ProductCatalog.Model;

namespace DimCorp.Cloud.Api
{
    public static class Converter
    {
        public static ApiProduct ToVm(this Product product)
        {
            return new ApiProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsAvailable = product.Availability > 0
            };
        }

        public static Product ToModel(this ApiProduct product)
        {
            return new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Availability = 100
            };
        }

        public static ApiBasketItem ToVm(this KeyValuePair<Guid, int> item)
        {
            return new ApiBasketItem
            {
                ProductId = item.Key.ToString(),
                Quantity = item.Value
            };
        }

        public static ApiBasket ToVm(this Dictionary<Guid, int> basket, string userId)
        {
            return new ApiBasket
            {
                UserId = userId,
                Items = basket.Select(ToVm).ToArray()
            };
        }

        public static ApiCheckoutSummary ToVm(this CheckoutSummary model)
        {
            return new ApiCheckoutSummary
            {
                Products = model.Products.Select(ToVm).ToList(),
                Date = model.Date,
                TotalPrice = model.TotalPrice
            };
        }

        public static ApiCheckoutProduct ToVm(this CheckoutProduct product)
        {
            return new ApiCheckoutProduct
            {
                ProductId = product.Product.Id,
                ProductName = product.Product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}
