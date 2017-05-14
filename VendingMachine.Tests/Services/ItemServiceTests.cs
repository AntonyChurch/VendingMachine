using System.Collections.Generic;
using Xunit;
using VendingMachine.Models;
using VendingMachine.Repositories;
using VendingMachine.Services;

namespace VendingMachine.Tests.Services
{
    public class ItemServiceTests
    {
        [Fact]
        public void ItemServiceReturnsFullCorrectListOfItemsFromRepository()
        {
            //If the repository spoke to a database it could be mocked with Moq or a similar tool.
            IItemRepository itemRepository = new ItemRepository();
            IItemService itemService = new ItemService(itemRepository);

            List<ItemModel> itemsFromRepository = itemRepository.GetAllItems();
            List<ItemModel> itemsFromService = itemService.GetItems();

            Assert.True(itemsFromRepository.Count == itemsFromService.Count);

            for(int i = 0; i < itemsFromRepository.Count; ++i)
            {
                Assert.True(itemsFromRepository[i].Id == itemsFromService[i].Id);
                Assert.True(itemsFromRepository[i].Name == itemsFromService[i].Name);
                Assert.True(itemsFromRepository[i].Price == itemsFromService[i].Price);
            }
        }
    }
}