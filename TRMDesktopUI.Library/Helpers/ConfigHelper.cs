using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        //move thise from config to the API
        public decimal GetTaxRate()
        {

            //string rateText =  ConfigurationManager.AppSettings["taxRate"];
            string rateText = "8.75";

            bool IsValidTaxRate = Decimal.TryParse(rateText, out decimal output);

            if (IsValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("Our Tax Rate is not set up properly");
            }

            return output;
        }
    }
}
