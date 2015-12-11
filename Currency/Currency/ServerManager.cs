using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Currency
{
    

    public class RateData
    {
        //private static string[] currencyList = { "USD", "EUR", "RUB", "BYR", "PLN", "UAH", "GBP", "CZK", "CAD", "SEK", "CNY", "JPY" };
        public string[] currencyList {get;}
        public string Symbol { get; set; }
        public Double Rate { get; set; }
        public string Range { get; set; }
        public string Date { get; set; }
    }

    class ServerManager
    {
        public static List<RateData> GetCurrencyInfo()
        {
            string csvData;

            using (WebClient web = new WebClient())
            {
                csvData = web.DownloadString(CreatePath());//("http://finance.yahoo.com/d/quotes.csv?e=.csv&s=USDRUB=X+USDEUR=X+USDBYR=X&f=sl1wd1");
            }
            if (csvData == null)
            {
                Console.WriteLine("Sorry/ ");
                return null;
            }
            else
                return Parse(csvData);
            /*
            Console.WriteLine(csvData);
            foreach (RateData tempRateData in rateInfo)
            {
                Console.WriteLine(string.Format("{0} Rate:{1}  Range:{2} Date:{3} ", tempRateData.Symbol, tempRateData.Rate, tempRateData.Range, tempRateData.Date));
            }

            Console.Read();
            */
        }

        private static List<RateData> Parse(string csvData)
        {
            List<RateData> RateInfo = new List<RateData>();

            string[] rows = csvData.Replace("\r", "").Split('\n');

            foreach (string row in rows)
            {
                if (string.IsNullOrEmpty(row)) continue;

                string[] cols = row.Split(',');

                RateData tempRateData = new RateData();
                tempRateData.Symbol = cols[0];//.Replace('"','');
                tempRateData.Rate = Convert.ToDouble(cols[1].Replace('.', ','));
                tempRateData.Range = cols[2];
                tempRateData.Date = cols[3];

                RateInfo.Add(tempRateData);
            }

            return RateInfo;
        }


        private static string CreatePath()
        {
            string[] currencyList = { "USD", "EUR", "RUB", "BYR", "PLN", "UAH", "GBP", "CZK", "CAD", "SEK", "CNY", "JPY" };
            string result = "http://finance.yahoo.com/d/quotes.csv?e=.csv&s=";
            foreach (string currencyFrom in currencyList)
            {
                foreach (string currencyTo in currencyList)
                {
                    if (currencyFrom != currencyTo)
                    {
                        result += currencyFrom + currencyTo + "=X+";
                    }
                }
            }
            result += "&f=sl1wd1";
            return result;
        }
    }
}

