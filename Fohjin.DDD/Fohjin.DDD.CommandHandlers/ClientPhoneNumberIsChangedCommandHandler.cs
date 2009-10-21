using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientPhoneNumberIsChangedCommandHandler : ICommandHandler<ClientPhoneNumberIsChangedCommand>
    {
        private readonly IDomainRepository _repository;

        public ClientPhoneNumberIsChangedCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ClientPhoneNumberIsChangedCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);

            client.UpdatePhoneNumber(new PhoneNumber(command.PhoneNumber));

            _repository.Save(client);
        }
    }
}