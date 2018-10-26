using System;

namespace TechnicalTest.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public bool LowFunds()
        {
            return Balance < 500m ? true : false;
        }

        public bool ApproachingPayInLimit(decimal amount)
        {

            return PayInLimit - amount < 500m ? true : false;
        }

        public void TransactionLessThenPayLimit(decimal amountToPayIn)
        {
            if (amountToPayIn > PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }
        }

        public void SufficientBalance(decimal amount)
        {
            if (Balance - amount <= 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

        }
    }
}
