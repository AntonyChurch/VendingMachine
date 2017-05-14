using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Services
{
    //Could be modified to accept search criteria/etc
    public interface IItemService
    {
        List<ItemModel> GetItems();
    }
}