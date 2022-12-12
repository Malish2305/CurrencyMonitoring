using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal Price { get; set; }
        public decimal Capitalisation { get; set; }
        public string? TokenAdress { get; set; }
        public decimal TotalSupply { get; set; }
        public int Rank { get; set; }
        public virtual ICollection<ChangesAccordingToTime> Delta { get; set; }
        public DateTime LastUpdate { get; set; }

        public Currency()
        {
            Delta = new List<ChangesAccordingToTime>();
        }

    }
}
