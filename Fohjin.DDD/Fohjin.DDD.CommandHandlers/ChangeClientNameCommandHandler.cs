using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Client;

namespace Fohjin.DDD.CommandHandlers
{
    public class ChangeClientNameCommandHandler : ICommandHandler<ChangeClientNameCommand>
    {
        private readonly IDomainRepository _repository;

        public ChangeClientNameCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangeClientNameCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);

            client.UpdateClientName(new ClientName(command.ClientName));

            _repository.Save(client);
        }
    }
}