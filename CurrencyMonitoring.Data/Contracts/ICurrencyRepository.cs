using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMCReceivingData.Models;
using CurrencyMonitoring.Data.Models;

namespace CurrencyMonitoring.Data.Abstracts
{
    public interface ICurrencyRepository
    {
        public void UpdateDB(CurrencyData[] currencies);
        public decimal? ConvertToSpecificCurrency(string currencyFrom, string currencyTo, decimal value);
        public List<decimal> ConvertToTop10(string currencyFrom, decimal value);
        public List<decimal> ConvertStartFromNUntilK(int currencyFrom, int n, int k, decimal value);
        public List<Currency> InformationAboutCurrencies();
        
    }
}
