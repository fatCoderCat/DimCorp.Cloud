﻿using System;
using Newtonsoft.Json;

namespace DimCorp.Cloud.Api.Model
{
    public class ApiProduct
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }
    }
}
