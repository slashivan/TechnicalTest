using TechnicalTest.App.DataAccess;
using TechnicalTest.App.Domain.Services;
using System;

namespace TechnicalTest.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:
            var fromAccount = this.accountRepository.GetAccountById(fromAccountId);

            fromAccount.SufficientBalance(amount);

            if (fromAccount.LowFunds())
            {
                this.notificationService.NotifyFundsLow(fromAccount.User.Email);
            }
           
            fromAccount.Balance =- amount;
            fromAccount.Withdrawn = -amount;

            this.accountRepository.Update(fromAccount);
        }
    }
}
