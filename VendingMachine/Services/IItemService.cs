using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Services
{
    public interface IItemService
    {
        List<ItemModel> GetItems();
    }
}