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

        public void Execute(CreateClientCommand compensatingCommand)
        {
            var client = Client.CreateNew(new ClientName(compensatingCommand.ClientName), new Address(compensatingCommand.Street, compensatingCommand.StreetNumber, compensatingCommand.PostalCode, compensatingCommand.City), new PhoneNumber(compensatingCommand.PhoneNumber));
            _repository.Save(client);
        }
    }
}