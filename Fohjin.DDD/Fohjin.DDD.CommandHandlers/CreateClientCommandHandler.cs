using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public CreateClientCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public void Execute(CreateClientCommand compensatingCommand)
        {
            var client = Client.CreateNew(new ClientName(compensatingCommand.ClientName), new Address(compensatingCommand.Street, compensatingCommand.StreetNumber, compensatingCommand.PostalCode, compensatingCommand.City), new PhoneNumber(compensatingCommand.PhoneNumber));

            _repository.Add(client);
        }
    }
}