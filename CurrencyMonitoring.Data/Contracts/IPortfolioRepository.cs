using CurrencyMonitoring.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Contracts
{
    public interface IPortfolioRepository
    {
        public void CreateNewPortfolio(string name, int userid);
        public void DeletePortfolio(string name, int userid);
        public void RealizeAllThePositiveProfit(string portfolioName, decimal value, int userId);
        public void RealizeAllNegativeProfit(string portfolioName, decimal value, int userId);
        public void RealizeProfitFromSpecificCurrecy(string portfolioName, string currency, decimal value, int userId);
        public void AddNewOrUpdateValueCurrencyInPortfolio(string potrfolioName, int userId, string currency, decimal value);
        public void AddNewOrUpdateValueCurrencyInPortfolio(string potrfolioName, int userId, string currency, decimal value, decimal purchasePrice);
        public List<PortfolioData> GetPortfolioInforamtion(string potrfolioName, int userId);
        public bool IsPortfolioExist(string portfolioName, int userId);

    }
}
