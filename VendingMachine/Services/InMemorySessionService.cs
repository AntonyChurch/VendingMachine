using System;
using System.Collections.Generic;

namespace VendingMachine.Services
{
    public class InMemorySessionService : ISessionService
    {
        private static Dictionary<Guid, double> _tallies;

        public InMemorySessionService()
        {
            if(_tallies == null)
            {
                _tallies = new Dictionary<Guid, double>();
            }
        }

        public void StoreCurrentTally(Guid sessionId, double currentValue)
        {
            if(_tallies.ContainsKey(sessionId))
            {
                _tallies.Remove(sessionId);
            }

            _tallies.Add(sessionId, currentValue);
        }

        public double GetStoredTally(Guid sessionId)
        {
            if(_tallies.ContainsKey(sessionId))
            {
                return _tallies[sessionId];
            }

            return -1;
        }
    }
}