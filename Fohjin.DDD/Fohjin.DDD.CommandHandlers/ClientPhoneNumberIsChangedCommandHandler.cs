using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientPhoneNumberIsChangedCommandHandler : ICommandHandler<ClientPhoneNumberIsChangedCommand>
    {
        private readonly IRepository _repository;

        public ClientPhoneNumberIsChangedCommandHandler(IRepository repository)
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