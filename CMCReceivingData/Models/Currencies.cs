using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMCReceivingData.Models
{
    public class Currencies
    {
        [JsonPropertyName("status")]
        public Status Status { get; set; }
        [JsonPropertyName("data")]
        public CurrencyData[] Data { get; set; }
    }
}
