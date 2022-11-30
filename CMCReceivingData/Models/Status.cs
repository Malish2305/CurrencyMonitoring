using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMCReceivingData.Models
{
    public class Status
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }
        [JsonPropertyName("error_message")]
        public object ErrorMessage { get; set; }
        [JsonPropertyName("elapsed")]
        public int Elapsed { get; set; }
        [JsonPropertyName("credit_count")]
        public int CreditCount { get; set; }
        [JsonPropertyName("notice")]
        public object Notice { get; set; }
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }
}