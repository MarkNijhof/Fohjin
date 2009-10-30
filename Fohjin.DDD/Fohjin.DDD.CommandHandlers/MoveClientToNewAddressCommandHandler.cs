using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Client;

namespace Fohjin.DDD.CommandHandlers
{
    public class MoveClientToNewAddressCommandHandler : ICommandHandler<MoveClientToNewAddressCommand>
    {
        private readonly IDomainRepository _repository;

        public MoveClientToNewAddressCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(MoveClientToNewAddressCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);

            client.ClientMoved(new Address(command.Street, command.StreetNumber, command.PostalCode, command.City));

            _repository.Save(client);
        }
    }
}