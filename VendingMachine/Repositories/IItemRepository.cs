using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Repositories
{
    public interface IItemRepository
    {
        List<ItemModel> GetAllItems();
        ItemModel GetItem(int itemId);
    }
}