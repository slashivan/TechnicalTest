using TechnicalTest.App.DataAccess;
using TechnicalTest.App.Domain.Services;
using System;
using TechnicalTest.App.Features.Interfaces;

namespace TechnicalTest.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;
        private IWithdrawlService withDrawMoneyService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService, IWithdrawlService withDrawMoneyService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
            this.withDrawMoneyService = withDrawMoneyService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var toAccount = this.accountRepository.GetAccountById(toAccountId);

            withDrawMoneyService.Execute(fromAccountId, amount);

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
