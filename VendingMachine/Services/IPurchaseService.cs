using System;

namespace VendingMachine.Services
{
    public interface IPurchaseService
    {
        //Returns the remaining money after a purchase test
        double CanAffordProduct(Guid sessionId, int itemId);
        double BuyProduct(Guid sessionId, int itemId);
    }
}