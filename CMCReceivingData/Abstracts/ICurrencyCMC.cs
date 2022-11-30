using CMCReceivingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCReceivingData.Abstracts
{
    public interface ICurrencyCMC
    {
        public void UpdateCurrencyInfo();
        public Currencies CurrenciesStatus();
       
    }
}
