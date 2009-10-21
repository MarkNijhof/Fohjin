using System;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.ActiveAccount;
using Moq;

namespace Test.Fohjin.DDD.Events
{
    public class Providing_an_money_transfered_to_an_other_account_event_for_back_into_the_domain : EventTestFixture<MoneyTransferedToAnOtherAccountEvent, MoneyTransferedReceivedFromAnOtherAccountEventHandler>
    {
        private static Guid _accountId;
        private TransferMoneyFromAnOtherAccountCommand _command;

        protected override void MockSetup()
        {
            GetMock<ICommandBus>()
                .Setup(x => x.Publish(It.IsAny<TransferMoneyFromAnOtherAccountCommand>()))
                .Callback<TransferMoneyFromAnOtherAccountCommand>(a => _command = a);
        }

        protected override MoneyTransferedToAnOtherAccountEvent When()
        {
            _accountId = Guid.NewGuid();
            var clientCreatedEvent = new MoneyTransferedToAnOtherAccountEvent(50.0M, 10.5M, "1234567890") { AggregateId = _accountId };
            return clientCreatedEvent;
        }

        [Then]
        public void Then_it_will_call_the_bus_is_called_to_send_the_command()
        {
            GetMock<ICommandBus>()
                .Verify(x => x.Publish(It.IsAny<TransferMoneyFromAnOtherAccountCommand>()));
        }

        [Then]
        public void Then_it_will_call_the_bus_with_the_correct_values_to_initiate_the_transfer()
        {
            _command.Id.WillBe(_accountId);
            _command.Amount.WillBe(10.5M);
            _command.AccountNumber.WillBe("1234567890");
        }
    }
}