using System.Collections.Generic;
using System.Linq;
using Xunit;
using VendingMachine.Currencies;
using VendingMachine.Services;

namespace VendingMachine.Tests.Services
{
    public class ChangeServiceTests
    {
        [Fact]
        public void CorrectChangeIsGivenForValues()
        {
            ICurrency currency = new USDCurrency();
            IChangeService changeService = new ChangeService(currency);

            List<CurrencyItem> change;

            //Changing $1 returns $1
            change = changeService.ChangeList(1.00);
            Assert.True(change.Count == 1);
            Assert.True(change[0].Value == 1);

            //Changing $1.50 returns $1 and $0.50
            change = changeService.ChangeList(1.50);
            Assert.True(change.Count == 2);
            Assert.True(change[0].Value == 1);
            Assert.True(change[1].Value == 0.5);

            //Chaning $0.04 returns 2 $0.02
            change = changeService.ChangeList(0.04);
            Assert.True(change.Count == 2, "Expected 2 Got " + change.Count);
            Assert.True(0.04 == change.Sum(c => c.Value), "Expected 0.04 got " + change.Sum(c => c.Value));
            Assert.True(change[0].Value == 0.02);
            Assert.True(change[1].Value == 0.02);
        }
    }
}