using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Models
{
    public class CurrencyPortfolio
    {
        public int CurrencyPortfolioId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ProtfolioName { get; set; }
        public decimal PortfolioTotalValue { get; set; }
        public decimal PortfolioTotalSpend { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal RealisedProfit { get; set; }
        public ICollection<PortfolioData> PortfolioData { get; set; }
        public CurrencyPortfolio()
        {
            PortfolioData = new List<PortfolioData>();
        }
    }
}
