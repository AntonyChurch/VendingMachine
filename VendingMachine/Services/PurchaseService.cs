using System;
using VendingMachine.Models;
using VendingMachine.Repositories;

namespace VendingMachine.Services
{
    public class PurchaseService : IPurchaseService
    {
        private IItemRepository _itemRepository;
        private ISessionService _sessionService;

        public PurchaseService(
            IItemRepository itemRepository,
            ISessionService sessionService)
        {
            _itemRepository = itemRepository;
            _sessionService = sessionService;   
        }

        public double CanAffordProduct(Guid sessionId, int itemId)
        {
            double userTotal = _sessionService.GetStoredTally(sessionId);                       

            //If usertotal is not in the cache (returns a -1), 
            //Store a new value as 0 in the session.
            if(userTotal < 0)
            {
                _sessionService.StoreCurrentTally(sessionId, 0);
                userTotal = 0;
            }

            ItemModel item = _itemRepository.GetItem(itemId);

            return userTotal - item.Price;
        }

        public double BuyProduct(Guid sessionId, int itemId)
        {
            double userTotal = _sessionService.GetStoredTally(sessionId); 
            ItemModel item = _itemRepository.GetItem(itemId);

            double newTotal = userTotal - item.Price;

            //Store the new user total in the session.
            _sessionService.StoreCurrentTally(sessionId, newTotal);
            
            return newTotal;
        }
    }
}