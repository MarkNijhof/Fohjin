﻿using System;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD.Scenarios.Assign_new_bank_card
{
    public class When_reporting_a_disabled_bank_card_stolen : CommandTestFixture<ReportStolenBankCardCommand, ReportStolenBankCardCommandHandler, Client>
    {
        private readonly Guid _bankCardId = Guid.NewGuid();
        private readonly Guid _accountId = Guid.NewGuid();
        private readonly Guid _clientId = Guid.NewGuid();

        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new ClientCreatedEvent(_clientId, "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountToClientAssignedEvent(_accountId)).ToVersion(2);
            yield return PrepareDomainEvent.Set(new NewBankCardForAccountAsignedEvent(_bankCardId, _accountId)).ToVersion(3);
            yield return PrepareDomainEvent.Set(new BankCardWasReportedStolenEvent { AggregateId = _bankCardId }).ToVersion(4);
        }

        protected override ReportStolenBankCardCommand When()
        {
            return new ReportStolenBankCardCommand(_clientId, _bankCardId);
        }

        [TestMethod]
        public void Then_a_non_existing_bank_card_is_disabled_will_be_thrown()
        {
            CaughtException.WillBeOfType<BankCardIsDisabledException>();
        }

        [TestMethod]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The bank card is disabled and no opperations can be executed on it");
        }
    }
}