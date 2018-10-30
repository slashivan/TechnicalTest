using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalTest.App.Domain.Services
{
    public interface IWithdrawlService
    {
        void Execute(Guid fromAccountId, decimal amount);
    }
}
