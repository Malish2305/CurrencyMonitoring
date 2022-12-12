using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Models
{
    public class ChangesAccordingToTime
    {
        public int ChangesAccordingToTimeId { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public string CurrencyName { get; set; }
        public decimal  Price { get; set; }
        public DateTime UpdateTime { get; set; }
        public decimal ChangeInPercentAfterLatestUpdate { get; set; }


    }
}
