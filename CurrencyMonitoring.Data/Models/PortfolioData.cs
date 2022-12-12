using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Models
{
    public class PortfolioData
    {
        public int PortfolioDataId { get; set; }
        public int CurrencyPortfolioId { get; set; }
        public CurrencyPortfolio CurrencyPortfolio { get; set; }
        public string Currency { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Value { get; set; }
    }
}
