using VendingMachine.Currencies;
using VendingMachine.ViewModels;

namespace VendingMachine.Services
{
    public class CoinValueService : ICoinValueService
    {
        private ICurrency _currency;

        public CoinValueService(ICurrency curreny)
        {
            _currency = curreny;
        }

        public CoinValueViewModel GetCoinValue(int coinWeight, int coinSize)
        {
            CoinValueViewModel model = new CoinValueViewModel();

            foreach(CurrencyItem currencyItem in _currency.GetItems())
            {
                if(currencyItem.MaxWeight > coinWeight
                && currencyItem.MinWeight < coinWeight
                && currencyItem.MaxSize > coinSize
                && currencyItem.MinSize < coinSize)
                {
                    model.CoinValue = currencyItem.Value;
                }
            }

            return model;
        }
    }
}