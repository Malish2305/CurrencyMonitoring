using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("portfolio")]
        public IEnumerable<CurrencyPortfolio> Portfolio { get; set; }

        public User()
        {
            Portfolio = new List<CurrencyPortfolio>();
        }
    }
}
