using TechnicalTest.App.DataAccess;
using TechnicalTest.App.Domain.Services;
using System;

namespace TechnicalTest.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var toAccount = this.accountRepository.GetAccountById(toAccountId);

            WithdrawMoney moneyWithdraw = new WithdrawMoney(accountRepository, notificationService);
            moneyWithdraw.Execute(fromAccountId, amount);

            toAccount.TransactionLessThenPayLimit(amount);

            if (toAccount.ApproachingPayInLimit(amount))
            {
                this.notificationService.NotifyApproachingPayInLimit(toAccount.User.Email);
            }

            toAccount.Balance =+ amount;
            toAccount.PaidIn =+ amount;

            this.accountRepository.Update(toAccount);
        }
    }
}
