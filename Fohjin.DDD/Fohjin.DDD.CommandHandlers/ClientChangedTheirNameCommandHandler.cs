using System;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientChangedTheirNameCommandHandler : ICommandHandler<ClientChangedTheirNameCommand>
    {
        private readonly IRepository _repository;

        public ClientChangedTheirNameCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ClientChangedTheirNameCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);

            client.UpdateClientName(new ClientName(command.ClientName));

            _repository.Save(client);
        }
    }
}