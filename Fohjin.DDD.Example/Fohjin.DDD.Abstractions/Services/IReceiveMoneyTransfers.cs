using Fohjin.DDD.Services.Models;

namespace Fohjin.DDD.Services
{
    public interface IReceiveMoneyTransfers
    {
        void Receive(MoneyTransfer moneyTransfer);
    }
}