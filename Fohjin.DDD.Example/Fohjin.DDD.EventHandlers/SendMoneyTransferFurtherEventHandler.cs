using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Services;
using Fohjin.DDD.Services.Models;

namespace Fohjin.DDD.EventHandlers
{
    public class SendMoneyTransferFurtherEventHandler : EventHandlerBase<MoneyTransferSendEvent>
    {
        private readonly ISendMoneyTransfer _sendMoneyTransfer;

        public SendMoneyTransferFurtherEventHandler(ISendMoneyTransfer sendMoneyTransfer)
        {
            _sendMoneyTransfer = sendMoneyTransfer;
        }

        public override Task ExecuteAsync(MoneyTransferSendEvent theEvent)
        {
            _sendMoneyTransfer.Send(new MoneyTransfer(theEvent.SourceAccount, theEvent.TargetAccount, theEvent.Amount));

            return Task.CompletedTask;
        }
    }
}