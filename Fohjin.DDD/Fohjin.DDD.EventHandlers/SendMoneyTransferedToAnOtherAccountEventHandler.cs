using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Services;

namespace Fohjin.DDD.EventHandlers
{
    public class SendMoneyTransferedToAnOtherAccountEventHandler : IEventHandler<MoneyTransferSendToAnOtherAccountEvent>
    {
        private readonly ISendMoneyTransfer _sendMoneyTransfer;

        public SendMoneyTransferedToAnOtherAccountEventHandler(ISendMoneyTransfer sendMoneyTransfer)
        {
            _sendMoneyTransfer = sendMoneyTransfer;
        }

        public void Execute(MoneyTransferSendToAnOtherAccountEvent theEvent)
        {
            _sendMoneyTransfer.Send(new MoneyTransfer(theEvent.SourceAccount, theEvent.TargetAccount, theEvent.Amount));
        }
    }
}