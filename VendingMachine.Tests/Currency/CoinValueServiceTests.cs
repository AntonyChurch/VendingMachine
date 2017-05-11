using Xunit;
using VendingMachine.Currencies;
using VendingMachine.Services;
using VendingMachine.ViewModels;

namespace VendingMachine.Tests.Currency
{
    public class CoinValueServiceTests
    {
        [Fact]
        public void CorrectInputProducesCorrectOutputUSDCurrency()
        {
            //Create a service with the USD Currency (could mock in future)
            ICoinValueService service = new CoinValueService(new USDCurrency());

            CoinValueViewModel coinValue = service.GetCoinValue(2.5, 19.05);
            Assert.True(coinValue.IsValidCoin);
            Assert.True(coinValue.CoinValue == 0.01);

            coinValue = service.GetCoinValue(5, 21.209);
            Assert.True(coinValue.IsValidCoin);
            Assert.True(coinValue.CoinValue == 0.05);

            coinValue = service.GetCoinValue(2.268, 17.907);
            Assert.True(coinValue.IsValidCoin);
            Assert.True(coinValue.CoinValue == 0.10);

            coinValue = service.GetCoinValue(5.67, 24.257);
            Assert.True(coinValue.IsValidCoin);
            Assert.True(coinValue.CoinValue == 0.25);

            coinValue = service.GetCoinValue(11.34, 30.607);
            Assert.True(coinValue.IsValidCoin);
            Assert.True(coinValue.CoinValue == 0.50);

            coinValue = service.GetCoinValue(22.68, 38.1);
            Assert.True(coinValue.IsValidCoin);
            Assert.True(coinValue.CoinValue == 1);
        }

        [Fact]
        public void IncorrectInputProducesInvalidCoin()
        {
            //Create a service with the USD Currency (could mock in future)
            ICoinValueService service = new CoinValueService(new USDCurrency());

            CoinValueViewModel coinValue = service.GetCoinValue(0, 0);
            Assert.True(coinValue.IsValidCoin == false);

            coinValue = service.GetCoinValue(-1, 0);
            Assert.True(coinValue.IsValidCoin == false);

            coinValue = service.GetCoinValue(-1, 1);
            Assert.True(coinValue.IsValidCoin == false);

            coinValue = service.GetCoinValue(0, -1);
            Assert.True(coinValue.IsValidCoin == false);

            coinValue = service.GetCoinValue(1, -1);
            Assert.True(coinValue.IsValidCoin == false);

            coinValue = service.GetCoinValue(1, 1);
            Assert.True(coinValue.IsValidCoin == false);

            coinValue = service.GetCoinValue(-1, -1);
            Assert.True(coinValue.IsValidCoin == false);
        }
    }
}