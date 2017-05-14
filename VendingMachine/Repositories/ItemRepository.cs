using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public List<ItemModel> GetAllItems()
        {
            List<ItemModel> items = new List<ItemModel>();

            items.Add(new ItemModel(){
                Id = 0,
                Name = "cola",
                Price = 1.00
            });

            items.Add(new ItemModel(){
                Id = 1,
                Name = "chips",
                Price = 0.50
            });

            items.Add(new ItemModel(){
                Id = 2,
                Name = "candy",
                Price = 0.65
            });

            return items;
        }
        
        public ItemModel GetItem(int itemId)
        {
            return GetAllItems().Where(i => i.Id == itemId).FirstOrDefault();
        }
    }
}