using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class AssignNewBankCardCommandHandler : ICommandHandler<AssignNewBankCardCommand>
    {
        private readonly IDomainRepository _repository;

        public AssignNewBankCardCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(AssignNewBankCardCommand assignNewCancelReportStolenBankCardCommand)
        {
            var client = _repository.GetById<Client>(assignNewCancelReportStolenBankCardCommand.Id);
            client.AssignNewBankCardForAccount(assignNewCancelReportStolenBankCardCommand.AccountId);
            _repository.Add(client);
        }
    }
}