using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientHasMovedCommandHandler : ICommandHandler<ClientHasMovedCommand>
    {
        private readonly IRepository<Client> _repository;

        public ClientHasMovedCommandHandler(IRepository<Client> repository)
        {
            _repository = repository;
        }

        public void Execute(ClientHasMovedCommand command)
        {
            var client = _repository.GetById(command.Id);

            client.ClientMoved(new Address(command.Street, command.StreetNumber, command.PostalCode, command.City));

            _repository.Save(client);
        }
    }
}