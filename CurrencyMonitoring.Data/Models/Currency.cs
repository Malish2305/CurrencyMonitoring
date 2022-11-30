using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring_CM_.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal Price { get; set; }
        public decimal Capitalisation { get; set; }
        public string TokenAdress { get; set; }
        public string Category { get; set; }
        public virtual IEnumerable<ChangesAccordingToTime> Delta { get; set; }
        public DateTime LastUpdate { get; set; }

        public Currency()
        {
            Delta = new List<ChangesAccordingToTime>();
        }

    }
}
