using CurrencyMonitoring.Data.Abstracts;
using CurrencyMonitoring.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMCReceivingData.Models;
using CurrencyMonitoring.Data.Models;
using System.Data.Entity;

namespace CurrencyMonitoring.Data.Repository
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyDbContext _dbContext;
        public CurrencyRepository(CurrencyDbContext currencyDbContext)
        {
            _dbContext = currencyDbContext;
        }

        public CurrencyRepository() : this(new CurrencyDbContext())
        {
        }



        public void UpdateState<T>(T newOne)
        {
            var old = _dbContext.Currencies.Find(newOne);

            _dbContext.Entry(old).CurrentValues.SetValues(newOne);
            _dbContext.Entry(old).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void AddNewCurrency(CurrencyData currency)
        {
            _dbContext.Currencies.Add(new Currency
            {
                CurrencyName = currency.Name,
                CurrencySymbol = currency.Symbol,
                Price = currency.Quote.USD.Price,
                Capitalisation = currency.Quote.USD.MarketCap,
                TokenAdress = currency.Platform?.TokenAddress,
                TotalSupply = currency.TotalSupply,
                Rank = currency.CMCRank,
                LastUpdate = DateTime.Now
            });

            _dbContext.SaveChanges();

        }

        public decimal PortfolioValue(CurrencyPortfolio currencyPortfolio)
        {
            return _dbContext.PortfolioData.Where(x => x.CurrencyPortfolioId == currencyPortfolio.CurrencyPortfolioId).Select(y => y.Value * _dbContext.Currencies.Find(y.Currency).Price).Sum();
        }

        public void UpdateDB(CurrencyData[] currencies)
        {
            foreach (var currency in currencies)
            {
                var dbcurrency = _dbContext.Currencies.FirstOrDefault(c => c.CurrencyName == currency.Name);

                if (dbcurrency != null)
                {
                    dbcurrency.Delta.Add(new ChangesAccordingToTime()
                    {
                        ChangeInPercentAfterLatestUpdate = (dbcurrency.Price - currency.Quote.USD.Price) / 100,
                        CurrencyName = currency.Name,
                        UpdateTime = DateTime.Now,
                        Price = dbcurrency.Price,
                    });

                    dbcurrency.Price = currency.Quote.USD.Price;
                    dbcurrency.TotalSupply = currency.TotalSupply;
                    dbcurrency.Capitalisation = currency.Quote.USD.MarketCap;
                    dbcurrency.LastUpdate = DateTime.Now;
                    dbcurrency.Rank = currency.CMCRank;

                    UpdateState<Currency>(dbcurrency);
                }


                if (dbcurrency == null)
                {
                    AddNewCurrency(currency);
                }
            }

            var dbportfolios = _dbContext.CurrencyPortfolios.Select(x => x).ToList();
            foreach (var portfolio in dbportfolios)
            {
                portfolio.PortfolioTotalValue = PortfolioValue(portfolio);
                UpdateState<CurrencyPortfolio>(portfolio);
            }

        }

        public decimal Converter(Currency from, Currency to, decimal value)
        {
            return from.Price / to.Price * value;
        }

        public decimal? ConvertToSpecificCurrency(string currencyFrom, string currencyTo, decimal value)
        {
            var currencyFromdb = _dbContext.Currencies.FirstOrDefault(x => x.CurrencyName == currencyFrom);
            var currencyTodb = _dbContext.Currencies.FirstOrDefault(j => j.CurrencyName == currencyTo);

            if (currencyFromdb == null || currencyTodb == null)
            {
                return null;
            }

            if (currencyFromdb.TotalSupply <= value || currencyTodb.TotalSupply <= Converter(currencyFromdb, currencyTodb, value))
            {
                return null;
            }

            return Converter(currencyFromdb, currencyTodb, value);
        }

        public List<decimal> ConvertToTop10(string currencyFrom, decimal value)
        {
            var top10Currencies = _dbContext.Currencies.Where(x => x.Rank <= 10).ToList();
            var currencyFromdb = _dbContext.Currencies.FirstOrDefault(j => j.CurrencyName == currencyFrom);

            if (top10Currencies == null || currencyFromdb == null)
            {
                return new List<decimal>();
            }

            var result = new List<Decimal>();
            foreach (var currency in top10Currencies)
            {
                result.Add(Converter(currencyFromdb, currency, value));
            }

            return result;
        }

        public List<decimal> ConvertStartFromNUntilK(int currencyFrom, int n, int k, decimal value)
        {
            throw new NotImplementedException();
        }

        public List<Currency> InformationAboutCurrencies()
        {
            return _dbContext.Currencies.ToList();
        }
    }
}
