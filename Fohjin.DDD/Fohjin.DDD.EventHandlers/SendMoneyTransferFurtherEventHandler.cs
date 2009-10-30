using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Services;

namespace Fohjin.DDD.EventHandlers
{
    public class SendMoneyTransferFurtherEventHandler : IEventHandler<MoneyTransferSendEvent>
    {
        private readonly ISendMoneyTransfer _sendMoneyTransfer;

        public SendMoneyTransferFurtherEventHandler(ISendMoneyTransfer sendMoneyTransfer)
        {
            _sendMoneyTransfer = sendMoneyTransfer;
        }

        public void Execute(MoneyTransferSendEvent theEvent)
        {
            _sendMoneyTransfer.Send(new MoneyTransfer(theEvent.SourceAccount, theEvent.TargetAccount, theEvent.Amount));
        }
    }
}