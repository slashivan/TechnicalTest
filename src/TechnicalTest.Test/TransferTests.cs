using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using TechnicalTest.App;

namespace TechnicalTest.Test
{
    public class TransferTests
    {
        [SetUp]
        public void TestSetup()
        {
            account = new Account();
        }

        private Account account;

        [Test]
        public void TestInsufficientFunds()
        {
            Assert.That(account.LowFunds(), Is.True);
        }

        [Test]
        public void TestSufficientFunds()
        {
            account.Balance = 550m;
            Assert.That(account.LowFunds(), Is.False);
        }

        [Test]
        public void TestApproachingPayInLimit()
        {
            Assert.That(account.ApproachingPayInLimit(3501m), Is.True);
        }

        [Test]
        public void TestNotApproachingPayInLimit()
        {
            Assert.That(account.ApproachingPayInLimit(3500m), Is.False);
        }

        [Test]
        public void TestTransactionMoreThenPayLimit()
        {
            var ex = Assert.Throws<InvalidOperationException>(
                () => account.TransactionLessThenPayLimit(4001m));
            Assert.IsTrue(ex.Message.Contains("Account pay in limit reached"));
        }

        [Test]
        public void TestTransactionLessThenPayLimit()
        {
            Assert.DoesNotThrow(() => account.TransactionLessThenPayLimit(4000m));
        }

        [Test]
        public void TestInsufficientBalance()
        {
            account.Balance = 100m;
            var ex = Assert.Throws<InvalidOperationException>(
                () => account.SufficientBalance(101m));
            Assert.IsTrue(ex.Message.Contains("Insufficient funds to make transfer"));
        }

        [Test]
        public void TestSufficientBalance()
        {
            account.Balance = 101m;
            Assert.DoesNotThrow(() => account.SufficientBalance(100m));
        }
    }
}
