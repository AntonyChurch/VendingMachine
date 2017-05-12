using System;
using Xunit;
using VendingMachine.Services;

namespace VendingMachine.Tests.Services
{
    public class InMemorySessionServiceTests
    {
        [Fact]
        public void InMemorySessionServiceStoresValues()
        {
            ISessionService service = new InMemorySessionService();
            Guid sessionId = Guid.NewGuid();
            service.StoreCurrentTally(sessionId, 1);

            Assert.True(service.GetStoredTally(sessionId) == 1);

            service.StoreCurrentTally(sessionId, 2);

            Assert.True(service.GetStoredTally(sessionId) == 2);
        }

        [Fact]
        public void InMemorySessionServiceReturnsNegativeOneIfInvalidSession()
        {
            ISessionService service = new InMemorySessionService();
            Guid sessionId = Guid.NewGuid();
            service.StoreCurrentTally(sessionId, 1);

            Assert.True(service.GetStoredTally(sessionId) == 1);
            Assert.True(service.GetStoredTally(Guid.Empty) == -1);

        }
    }
}