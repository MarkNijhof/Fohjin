using Fohjin.DDD.Commands;
using Fohjin.DDD.Common;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class OpenNewAccountForClientCommandHandler : ICommandHandler<OpenNewAccountForClientCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository; 
        private readonly ISystemHash _systemHash;

        public OpenNewAccountForClientCommandHandler(
            IDomainRepository<IDomainEvent> repository,
            ISystemHash systemHash
            )
        {
            _repository = repository;
            _systemHash = systemHash;
        }

        public void Execute(OpenNewAccountForClientCommand compensatingCommand)
        {
            var client = _repository.GetById<Client>(compensatingCommand.Id);
            var activeAccount = client.CreateNewAccount(compensatingCommand.AccountName, _systemHash.Hash(compensatingCommand.AccountName));

            _repository.Add(activeAccount);
        }
    }
}