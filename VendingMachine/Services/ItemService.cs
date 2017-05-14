using System.Collections.Generic;
using VendingMachine.Models;
using VendingMachine.Repositories;

namespace VendingMachine.Services
{
    public class ItemService : IItemService
    {
        private IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        //Could be used to turn a big ItmeModel into a more concise DTO object/View Model     
        //ItemModel is very simple and small, there would be nothing to gain from converting here.
        //But the infrastructure is in place.
        public List<ItemModel> GetItems()
        {
            return _itemRepository.GetAllItems();
        }
    }
}