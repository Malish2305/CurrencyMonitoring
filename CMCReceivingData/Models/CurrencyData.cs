using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMCReceivingData.Models
{
    public class CurrencyData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("num_market_pairs")]
        public int NumMarketPairs { get; set; }
        [JsonPropertyName("date_added")]
        public DateTime DateAdded { get; set; }
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
        [JsonPropertyName("max_supply")]
        public long? MaxSupply { get; set; }
        [JsonPropertyName("circulating_supply")]
        public float CirculatingSupply { get; set; }
        [JsonPropertyName("total_supply")]
        public float TotalSupply { get; set; }
        [JsonPropertyName("platform")]
        public Platform Platform { get; set; }
        [JsonPropertyName("cmc_rank")]
        public int CMCRank { get; set; }
        [JsonPropertyName("self_reported_circulating_supply")]
        public float? SelfReportedCirculatingSupply { get; set; }
        [JsonPropertyName("self_reported_market_cap")]
        public float? SelfReportedMarketCap { get; set; }
        [JsonPropertyName("tvl_ratio")]
        public float? TVL { get; set; }
        [JsonPropertyName("last_update")]
        public DateTime LastUpdated { get; set; }
        [JsonPropertyName("quote")]
        public Quote Quote { get; set; }

    }
}

