using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Client;

namespace Fohjin.DDD.CommandHandlers
{
    public class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IDomainRepository _repository;

        public CreateClientCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CreateClientCommand command)
        {
            var client = Client.CreateNew(new ClientName(command.ClientName), new Address(command.Street, command.StreetNumber, command.PostalCode, command.City), new PhoneNumber(command.PhoneNumber));
            _repository.Save(client);
        }
    }
}