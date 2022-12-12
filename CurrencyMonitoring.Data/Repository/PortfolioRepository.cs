using CurrencyMonitoring.Data.Contracts;
using CurrencyMonitoring.Data.EF;
using CurrencyMonitoring.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitoring.Data.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly CurrencyDbContext _dbContext;
        public PortfolioRepository(CurrencyDbContext currencyDbContext)
        {
            _dbContext = currencyDbContext;
        }

        public PortfolioRepository() : this(new CurrencyDbContext())
        {

        }

        public void CreateNewPortfolio(string portfolioName, int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(y => y.Id == userId);
            var portfolio = _dbContext.CurrencyPortfolios.FirstOrDefault(x => x.UserId == userId && x.ProtfolioName == portfolioName);
            if (user != null && portfolio == null)
            {
                _dbContext.CurrencyPortfolios.Add(new CurrencyPortfolio() { User = user, ProtfolioName = portfolioName });
                _dbContext.SaveChanges();
            }
        }

        public decimal PortfolioValue(CurrencyPortfolio currencyPortfolio)
        {
            return _dbContext.PortfolioData.Where(x => x.CurrencyPortfolioId == currencyPortfolio.CurrencyPortfolioId).Select(y => y.Value * _dbContext.Currencies.Find(y.Currency).Price).Sum();
        }

        public void UpdateState<T>(T newOne)
        {
            var old = _dbContext.Currencies.Find(newOne);

            _dbContext.Entry(old).CurrentValues.SetValues(newOne);
            _dbContext.Entry(old).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeletePortfolio(string portfolioName, int userId)
        {

            if (_dbContext.CurrencyPortfolios.FirstOrDefault(x => x.ProtfolioName == portfolioName && x.UserId == userId) != null)
            {
                try
                {
                    var portfolio = _dbContext.CurrencyPortfolios.FirstOrDefault(x => x.ProtfolioName == portfolioName);
                    var portfolioData = _dbContext.PortfolioData.Where(x => x.CurrencyPortfolioId == portfolio.CurrencyPortfolioId);
                    foreach (var data in portfolioData)
                    {
                        _dbContext.PortfolioData.Remove(data);
                    }
                    _dbContext.CurrencyPortfolios.Remove(portfolio);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void AddNewOrUpdateValueCurrencyInPortfolio(string portfolioName, int userId, string currency, decimal value)
        {
            var findPortfolio = _dbContext.CurrencyPortfolios.FirstOrDefault(x => x.ProtfolioName == portfolioName && x.UserId == userId);
            var findCurrencyInPortfolio = _dbContext.PortfolioData.FirstOrDefault(x => x.Currency == currency);
            var findCurrencyIndb = _dbContext.Currencies.Any(x => x.CurrencyName == currency);
            if (findPortfolio == null || findCurrencyIndb == false)
            {
                return;
            }

            if (findCurrencyInPortfolio != null)
            {
                findCurrencyInPortfolio.Value += value;
                UpdateState<PortfolioData>(findCurrencyInPortfolio);
            }

            if (findCurrencyInPortfolio == null)
            {
                _dbContext.PortfolioData.Add(new PortfolioData()
                {
                    Currency = currency,
                    CurrencyPortfolio = findPortfolio,
                    Value = value
                });
            }

            findPortfolio.PortfolioTotalValue = PortfolioValue(findPortfolio);
            UpdateState<CurrencyPortfolio>(findPortfolio);
        }

        public List<PortfolioData> GetPortfolioInforamtion(string potrfolioName, int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            var portfolio = _dbContext.CurrencyPortfolios.FirstOrDefault(x=>x.UserId == userId && x.ProtfolioName == potrfolioName);
            if(user == null || portfolio == null)
            {
                return new List<PortfolioData>();
            }
            return _dbContext.PortfolioData.Where(x=>x.CurrencyPortfolioId == portfolio.CurrencyPortfolioId).ToList();

        }

        public bool IsPortfolioExist(string portfolioName, int userId)
        {
            return _dbContext.CurrencyPortfolios.Any(x => x.ProtfolioName == portfolioName && x.UserId == userId);
        }
    }
}
