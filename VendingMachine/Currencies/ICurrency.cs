using System.Collections.Generic;

namespace VendingMachine.Currencies
{
    public interface ICurrency
    {
        List<CurrencyItem> GetItems();
    }
}