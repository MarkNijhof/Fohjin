using Fohjin.DDD.Services.Models;

namespace Fohjin.DDD.Services
{
    public interface ISendMoneyTransfer
    {
        void Send(MoneyTransfer moneyTransfer);
    }
}