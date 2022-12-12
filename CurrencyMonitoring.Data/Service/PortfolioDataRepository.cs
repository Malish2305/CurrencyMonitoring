using CurrencyMonitoring.Data.Contracts;
using CurrencyMonitoring.Data.Models;
using CurrencyMonitoring.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Service
{
    public class PortfolioDataRepository
    {
        IPortfolioRepository _portfolioRepository;

        public PortfolioDataRepository(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public PortfolioDataRepository() : this(new PortfolioRepository())
        {

        }

        public void CreateNewPortfolio(string portfolioName, int userId)
        {
            portfolioName = portfolioName.ToUpper();
            _portfolioRepository.CreateNewPortfolio(portfolioName, userId);
        }

        public void DeletePortfolio(string portfolioName, int userId)
        {
            portfolioName = portfolioName.ToUpper();
            _portfolioRepository.DeletePortfolio(portfolioName, userId);
        }
        public void AddNewOrUpdateValueCurrencyInPortfolio(string portfolioName, int userId, string currency, decimal value)
        {
            portfolioName = portfolioName.ToUpper();
            _portfolioRepository.AddNewOrUpdateValueCurrencyInPortfolio(portfolioName, userId, currency, value);
        }

        public List<PortfolioData> GetPortfolioInforamtion(string portfolioName, int userId)
        {
            portfolioName = portfolioName.ToUpper();
            return _portfolioRepository.GetPortfolioInforamtion(portfolioName, userId);
        }
        public bool IsPortfolioExist(string portfolioName, int userId)
        {
            portfolioName = portfolioName.ToUpper();
            return _portfolioRepository.IsPortfolioExist(portfolioName, userId);
        }


    }
}
