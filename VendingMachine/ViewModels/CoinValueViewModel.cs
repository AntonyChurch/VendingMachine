namespace VendingMachine.ViewModels
{
    public class CoinValueViewModel
    {        
        public double? CoinValue { get; set; }

        public string CoinValueString 
        {
            get
            {
                if(CoinValue.HasValue)
                {
                    return CoinValue.Value.ToString("C");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool IsValidCoin 
        { 
            get  
            {
                return CoinValue.HasValue;
            }
        }
    }
}