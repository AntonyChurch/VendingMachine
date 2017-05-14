using System;
using System.Collections.Generic;

namespace VendingMachine.ViewModels
{
    public class IndexViewModel
    {
        public Guid SessionId { get; set; }
        public double UserTotal { get; set; }
        public double CoinValue { get; set; }
        public string ItemName { get; set; }
        public double ItemCost { get; set; }
        public List<string> ItemsToCollect { get; set; }
        public List<string> CoinsToCollect { get; set; }

        public IndexViewModel()
        {
            ItemsToCollect = new List<string>();
            CoinsToCollect = new List<string>();
        }
    }
}