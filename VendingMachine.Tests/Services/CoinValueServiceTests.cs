using Xunit;
using VendingMachine.Currencies;
using VendingMachine.Services;
using VendingMachine.ViewModels;

namespace VendingMachine.Tests.Services
{
    public class CoinValueServiceTests
    {
        [Fact]
        public void CoinValueServiceReturnsCorrectValueForMinAndMaxEntries()
        {
            ICurrency currency = new USDCurrency();
            ICoinValueService coinService = new CoinValueService(currency);

            CurrencyItem firstCoin = currency.GetItems()[0];
            CoinValueViewModel coin;
            //Max weight
            coin = coinService.GetCoinValue(firstCoin.MaxWeight, AverageBetweenTwoValues(firstCoin.MinSize, firstCoin.MaxSize));
            Assert.True(coin.IsValidCoin);
            Assert.True(coin.CoinValue == firstCoin.Value);
            //Min weight
            coin = coinService.GetCoinValue(firstCoin.MinWeight, AverageBetweenTwoValues(firstCoin.MinSize, firstCoin.MaxSize));
            Assert.True(coin.IsValidCoin);
            Assert.True(coin.CoinValue == firstCoin.Value);
            //Max size
            coin = coinService.GetCoinValue(AverageBetweenTwoValues(firstCoin.MaxWeight, firstCoin.MinWeight), firstCoin.MaxSize);
            Assert.True(coin.IsValidCoin);
            Assert.True(coin.CoinValue == firstCoin.Value);
            //Min size
            coin = coinService.GetCoinValue(AverageBetweenTwoValues(firstCoin.MaxWeight, firstCoin.MinWeight), firstCoin.MinSize);
            Assert.True(coin.IsValidCoin);
            Assert.True(coin.CoinValue == firstCoin.Value);
        }

        private double AverageBetweenTwoValues(double val1, double val2)
        {
            return (val1 + val2) / 2;
        }
    }
}