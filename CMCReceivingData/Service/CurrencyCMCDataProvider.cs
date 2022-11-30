using CMCReceivingData.Abstracts;
using CMCReceivingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMCReceivingData.Service
{

    public class CurrencyCMCDataProvider: ICurrencyCMC
    {
        private readonly static string API_KEY = "a90a7b1e-165b-4ec0-8f82-61a3d8dd1067";

        private static Currencies currencies;
        private static DateTime LastUpdated = DateTime.Now;

        public void UpdateCurrencyInfo()
        {
            if (LastUpdated <= DateTime.Now.AddMinutes(-10) || currencies == null)
            {
                try
                {
                    LastUpdated = DateTime.Now;
                    var Json = CurrenciesStatusApiRequst();
                    currencies = JsonSerializer.Deserialize<Currencies>(Json);
                }
                catch (WebException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public Currencies CurrenciesStatus()
        {
            UpdateCurrencyInfo();
            return currencies;
        }


        public string CurrenciesStatusApiRequst()
        {
            var URL = new UriBuilder(@"pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }

        

    }
}
