using System.Collections.Generic;
using VendingMachine.Currencies;

namespace VendingMachine.Services
{
    public interface IChangeService
    {
        List<CurrencyItem> ChangeList(double total);
    }
}