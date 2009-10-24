using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferedReceivedFromAnOtherAccountEventHandler : IEventHandler<MoneyTransferSendToAnOtherAccountEvent>
    {
        private readonly ICommandBus _bus;

        public MoneyTransferedReceivedFromAnOtherAccountEventHandler(ICommandBus bus)
        {
            _bus = bus;
        }

        public void Execute(MoneyTransferSendToAnOtherAccountEvent theEvent)
        {
            _bus.Publish(new TransferMoneyFromAnOtherAccountCommand(theEvent.AggregateId, theEvent.Amount, theEvent.TargetAccount));
        }
    }
}