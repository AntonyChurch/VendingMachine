using System.Collections.Generic;
using System.Linq;
using VendingMachine.Currencies;

namespace VendingMachine.Services
{
    public class ChangeService : IChangeService
    {
        private ICurrency _currency;

        public ChangeService(ICurrency currency)
        {
            _currency = currency;
        }

        public List<CurrencyItem> ChangeList(double total)
        {
            List<CurrencyItem> changeItems = new List<CurrencyItem>();
            List<CurrencyItem> coins = _currency.GetItems().OrderByDescending(ci => ci.Value).ToList();

            double tally = total;
            while(tally != 0)
            {
                foreach(var CurrencyItem in coins)
                {
                    if(tally >= CurrencyItem.Value)
                    {
                        changeItems.Add(CurrencyItem);
                        tally -= CurrencyItem.Value;
                        break;
                    }
                }
            }

            return changeItems;
        }
    }
}