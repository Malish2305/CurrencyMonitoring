using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMCReceivingData.Service;
using CurrencyMonitoring.Data.Abstracts;
using CurrencyMonitoring.Data.Models;
using CurrencyMonitoring.Data.Repository;

namespace CurrencyMonitoring.Data.Service
{
    public class CurrencyDataProvider
    {
        ICurrencyRepository _currencyRepository;
        private readonly CurrencyCMCDataProvider _currencyCMCDataProvider;

        public CurrencyDataProvider(ICurrencyRepository currencyRepository, CurrencyCMCDataProvider currencyCMCDataProvider)
        {
            _currencyRepository = currencyRepository;
            _currencyCMCDataProvider = currencyCMCDataProvider;
        }

        public CurrencyDataProvider()
        {
            _currencyRepository = new CurrencyRepository();
            _currencyCMCDataProvider = new CurrencyCMCDataProvider();      
        }

        public void UpdateDB()
        {
            try
            {
                var currenciesCMC = _currencyCMCDataProvider.CurrenciesStatus();
                _currencyRepository.UpdateDB(currenciesCMC.Data);
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Bad Request to CMC");
            }
        }

        public decimal? ConvertToSpecificCurrency(string currencyFrom, string currencyTo, decimal value)
        {
           return _currencyRepository.ConvertToSpecificCurrency(currencyFrom, currencyTo, value);
        }

        public List<decimal> ConvertToTop10(string currencyFrom, decimal value)
        {
            return _currencyRepository.ConvertToTop10(currencyFrom, value);
        }

        public List<Currency> InformationAboutCurrencies()
        {
            return _currencyRepository.InformationAboutCurrencies();
        }

    }
}
