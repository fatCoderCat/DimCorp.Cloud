using System;
using System.Collections.Generic;

namespace DimCorp.Cloud.Checkout.Model
{
    public class CheckoutSummary
    {
        public List<CheckoutProduct> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }

    public static class CheckoutSummaryBuilder
    {
        public static CheckoutSummary Create()
        {
            return new CheckoutSummary
            {
                Date = DateTime.UtcNow,
                Products = new List<CheckoutProduct>()
            };
        }

        public static CheckoutSummary WithProduct(
            this CheckoutSummary chekcoutSummary, 
            CheckoutProduct product)
        {
            chekcoutSummary.Products.Add(product);
            chekcoutSummary.TotalPrice += product.Price * product.Quantity;
            return chekcoutSummary;
        }
    }
}
