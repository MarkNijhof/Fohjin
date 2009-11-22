using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientIsMovingCommandHandler : ICommandHandler<ClientIsMovingCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public ClientIsMovingCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public void Execute(ClientIsMovingCommand compensatingCommand)
        {
            var client = _repository.GetById<Client>(compensatingCommand.Id);

            client.ClientMoved(new Address(compensatingCommand.Street, compensatingCommand.StreetNumber, compensatingCommand.PostalCode, compensatingCommand.City));
        }
    }
}