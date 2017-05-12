using System;

namespace VendingMachine.Services
{
    // .Net Core ASP.NET Core has a built in session service,
    // however it was not functioning on my Linux install,
    // so I have had to hand roll a simple one.
    public interface ISessionService
    {
        void StoreCurrentTally(Guid sessionId, double currentValue);
        double GetStoredTally(Guid sessionId);
    }
}