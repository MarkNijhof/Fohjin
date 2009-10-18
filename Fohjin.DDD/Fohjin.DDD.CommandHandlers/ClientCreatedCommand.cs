using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientCreatedCommandHandler : ICommandHandler<ClientCreatedCommand>
    {
        private readonly IRepository _repository;

        public ClientCreatedCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ClientCreatedCommand command)
        {
            var client = Client.CreateNew(new ClientName(command.ClientName), new Address(command.Street, command.StreetNumber, command.PostalCode, command.City), new PhoneNumber(command.PhoneNumber));
            _repository.Save(client);
        }
    }
}