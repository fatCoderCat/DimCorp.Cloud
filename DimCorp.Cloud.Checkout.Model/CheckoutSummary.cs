﻿using System;
using System.Collections.Generic;

namespace DimCorp.Cloud.Checkout.Model
{
    public class CheckoutSummary
    {
        public List<CheckoutProduct> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
