using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring_CM_.Models
{
    public class ChangesAccordingToTime
    {
        public int ChangesAccordingToTimeId { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public double ChangeInPercentAfterLatestUpdate { get; set; }


    }
}
