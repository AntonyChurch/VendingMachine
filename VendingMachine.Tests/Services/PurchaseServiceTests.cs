using System;
using Xunit;
using VendingMachine.Models;
using VendingMachine.Repositories;
using VendingMachine.Services;

namespace VendingMachine.Tests.Services
{
    public class PurchaseServiceTests
    {
        [Fact]
        public void PurchaseServiceCanAffordProductWorksCorrectlyWhenSufficientFundsAreIn()
        {
            //These could be mocked out, but since they are simple in memory 
            //instaces there is no point.
            ISessionService sessionService = new InMemorySessionService();
            IItemRepository itemRepository = new ItemRepository();

            IPurchaseService purchaseService = new PurchaseService(itemRepository, sessionService);

            Guid sessionId = Guid.NewGuid();

            double tallyValue = 2;
            sessionService.StoreCurrentTally(sessionId, tallyValue);

            //If sufficient money is in a session, the item price minus the session tally is returned.
            double checkResult = tallyValue - itemRepository.GetItem(0).Price;
            double result = purchaseService.CanAffordProduct(sessionId, 0);

            Assert.True(result > 0);
            Assert.True(checkResult == result);
            //Make sure the session hasn't been modified
            Assert.True(sessionService.GetStoredTally(sessionId) == tallyValue);
        }

        [Fact]
        public void PurchaseServiceCanAffordProductWorksCorrectlyWhenInsufficientFundsAreIn()
        {
            //These could be mocked out, but since they are simple in memory 
            //instaces there is no point.
            ISessionService sessionService = new InMemorySessionService();
            IItemRepository itemRepository = new ItemRepository();

            IPurchaseService purchaseService = new PurchaseService(itemRepository, sessionService);

            Guid sessionId = Guid.NewGuid();

            double tallyValue = 0.5;
            sessionService.StoreCurrentTally(sessionId, tallyValue);

            //If sufficient money is in a session, the item price minus the session tally is returned.
            double checkResult = tallyValue - itemRepository.GetItem(0).Price;
            double result = purchaseService.CanAffordProduct(sessionId, 0);

            Assert.True(result < 0);
            Assert.True(checkResult == result);

            //Make sure the session hasn't been modified
            Assert.True(sessionService.GetStoredTally(sessionId) == tallyValue);
        }

        [Fact]
        public void PurchaseServiceBuyProductWorksCorrectly()
        {
            //These could be mocked out, but since they are simple in memory 
            //instaces there is no point.
            ISessionService sessionService = new InMemorySessionService();
            IItemRepository itemRepository = new ItemRepository();

            IPurchaseService purchaseService = new PurchaseService(itemRepository, sessionService);

            Guid sessionId = Guid.NewGuid();

            double initialTallyValue = 2;
            sessionService.StoreCurrentTally(sessionId, initialTallyValue);

            purchaseService.BuyProduct(sessionId, 1);

            Assert.True(sessionService.GetStoredTally(sessionId) == (initialTallyValue - itemRepository.GetItem(1).Price));
        }
    }
}