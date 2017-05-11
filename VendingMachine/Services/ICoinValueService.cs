using VendingMachine.ViewModels;

namespace VendingMachine.Services
{
    public interface ICoinValueService
    {
        CoinValueViewModel GetCoinValue(int coinWeight, int coinSize);
    }
}