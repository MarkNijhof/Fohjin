using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class OpenNewAccountForClientCommandHandler : ICommandHandler<OpenNewAccountForClientCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public OpenNewAccountForClientCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public void Execute(OpenNewAccountForClientCommand compensatingCommand)
        {
            var client = _repository.GetById<Client>(compensatingCommand.Id);
            var activeAccount = client.CreateNewAccount(compensatingCommand.AccountName);

            _repository.Add(activeAccount);
        }
    }
}