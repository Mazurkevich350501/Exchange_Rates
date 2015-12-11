using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency
{
    static class ConvertCurrency
    {
        public static double ConvertValue(string value, double rate)
        {
            double result;
            try{ result = Convert.ToInt32(value); }
            catch { return 0; }
            return result*rate;
        }
    }
}
