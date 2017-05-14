using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Services
{
    public class ItemService : IItemService
    {
        //Could use a repository or something similar to read from a database or a file.
        //Since there is a very small and limited scope these values have been hard coded.        
        public List<ItemModel> GetItems()
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
    }
}