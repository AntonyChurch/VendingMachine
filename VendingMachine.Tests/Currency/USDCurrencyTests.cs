using System;
using System.Collections.Generic;
using Xunit;
using VendingMachine.Currencies;

namespace VendingMachine.Tests.Currency
{
    public class USDCurrencyTests
    {
        [Fact]
        public void AreCurrencyItemsValid()
        {
            USDCurrency usd = new USDCurrency();

            List<CurrencyItem> currencyItems = usd.GetItems();

            foreach(CurrencyItem item in currencyItems)
            {
                Assert.True(item.MaxWeight > item.MinWeight, 
                    string.Format("Items MaxWeight is smaller than it's MinWeight, MinWeight {0}, MaxWeight {1} for value {2}", item.MinWeight, item.MaxWeight, item.Value));
                Assert.True(item.MaxSize > item.MinSize, 
                    string.Format("Items MaxSize is smaller than it's MinSize, MinSize {0}, MaxSize {1} for value {2}", item.MinSize, item.MaxSize, item.Value));
            }
        }

        [Fact]
        public void AreCurrencyItemsChecksValid()
        {
            USDCurrency usd = new USDCurrency();

            List<CurrencyItem> currencyItems = usd.GetItems();

            foreach(CurrencyItem currencyItem in currencyItems)
            {
                foreach(CurrencyItem checkItem in currencyItems)
                {          
                    //If the items match, move on.
                    if(currencyItem == checkItem)
                    {
                        continue;
                    }


                    if(currencyItem.MaxWeight < checkItem.MinWeight)
                    {
                        //checkItem has to be heavier than currencyItem
                        //Which means they cannot be mistaken
                        continue;
                    }

                    if(currencyItem.MinWeight > checkItem.MaxWeight)
                    {
                        //checkItem has to be lighter than currencyItem
                        //Which means they cannot be mistaken
                        continue;
                    }

                    if(currencyItem.MaxSize < checkItem.MinSize)
                    {
                        //checkItem has to be larger than currency item
                        //Which means they cannot be mistaken
                        continue;
                    }

                    if(currencyItem.MinSize > checkItem.MaxSize)
                    {
                        //checkItem has to be smaller than currency item
                        //Which means they cannot be mistaken
                        continue;
                    }

                    //Items can be mistaken.
                    string message = string.Format("Currency of value {0} and currency of value {1} can be mistaken for each other.", currencyItem.Value, checkItem.Value);
                    Assert.True(false, message);
                }
            }
        }
    }
}
