using System.Collections.Generic;

namespace VendingMachine.Currencies
{
    public class USDCurrency : ICurrency
    {
        private double _precentageDivisor = 100;

        //Values in this list are accoring to this Wikipedia page ignoring the last one because with the tollerances the two types of dollar are equal.
        //https://en.wikipedia.org/wiki/Coins_of_the_United_States_dollar
        //Each coins size and weight has +/- 1% of official value because coins are never quite equal
        public List<CurrencyItem> GetItems()
        {
            List<CurrencyItem> items = new List<CurrencyItem>();

            items.Add(GetCurrencyItem(2.5, 19.05, 0.01));

            items.Add(GetCurrencyItem(5, 21.209, 0.05));                

            items.Add(GetCurrencyItem(2.268, 17.907, 0.10));

            items.Add(GetCurrencyItem(5.67, 24.257, 0.25));

            items.Add(GetCurrencyItem(11.34, 30.607, 0.50));

            items.Add(GetCurrencyItem(22.68, 38.1, 1));

            items.Add(GetCurrencyItem(8.10, 26.5, 1));

            return items;
        }

        public CurrencyItem GetCurrencyItem(double weight, double size, double value)
        {
            CurrencyItem item = new CurrencyItem();

            item.MaxWeight = weight + (weight / _precentageDivisor);
            item.MinWeight = weight - (weight / _precentageDivisor);
            item.MaxSize = size + (size / _precentageDivisor);
            item.MinSize = size - (size / _precentageDivisor);
            item.Value = value;

            return item;
        }
    }
}