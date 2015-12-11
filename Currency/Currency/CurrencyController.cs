using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Currency;

namespace Currency
{
    class CurrencyController
    {
        private List<RateData> currencyInfo;

        public CurrencyController()
        {
            UpdateInfo();
        }

        public bool UpdateInfo()
        {
            if ((currencyInfo = ServerManager.GetCurrencyInfo()) != null)
                return true;
            else
                return false;
        }

        public List<RateData> GetCurrencyInfo(string currency)
        {
            List<RateData> result = new List<RateData>();
            foreach(RateData data in currencyInfo)
            {
                if(data.Symbol.Substring(1,3) == currency)
                {
                    RateData newItem = new RateData();
                    newItem = data;
                    result.Add(newItem);
                }
            }
            return result;
        }

        public double GetRate(string From, string To)
        {
            foreach (RateData data in currencyInfo)
            {
                if ((From + To) == data.Symbol.Substring(1, 6))
                    return data.Rate;
            }
            return 0;
        }
    }
}
