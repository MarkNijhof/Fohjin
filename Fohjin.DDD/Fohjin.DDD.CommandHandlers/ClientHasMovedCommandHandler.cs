using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientHasMovedCommandHandler : ICommandHandler<ClientHasMovedCommand>
    {
        private readonly IRepository _repository;

        public ClientHasMovedCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ClientHasMovedCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);

            client.ClientMoved(new Address(command.Street, command.StreetNumber, command.PostalCode, command.City));

            _repository.Save(client);
        }
    }
}