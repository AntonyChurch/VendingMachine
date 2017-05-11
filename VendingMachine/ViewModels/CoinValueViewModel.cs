namespace VendingMachine.ViewModels
{
    public class CoinValueViewModel
    {        
        public double? CoinValue { get; set; }

        public bool IsValidCoin 
        { 
            get  
            {
                return CoinValue.HasValue;
            }
        }
    }
}