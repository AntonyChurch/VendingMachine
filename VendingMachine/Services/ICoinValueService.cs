using VendingMachine.ViewModels;

namespace VendingMachine.Services
{
    public interface ICoinValueService
    {
        CoinValueViewModel GetCoinValue(double coinWeight, double coinSize);
    }
}