using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CurrencyMonitoring_CM_.Models;

namespace CurrencyMonitoring_CM_.EF
{
    public class CurrencyDbContext : DbContext 
    {
        public CurrencyDbContext(string connectionString) : base(connectionString)
        {

        }

        public CurrencyDbContext() : base()
        {

        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ChangesAccordingToTime> ChangesAccordingToTime { get; set; }
    }
}
