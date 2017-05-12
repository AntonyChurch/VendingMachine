using System;

namespace VendingMachine.Services
{
    public interface ISessionService
    {
        void StoreCurrentTally(Guid sessionId, double currentValue);
        double GetStoredTally(Guid sessionId);
    }
}