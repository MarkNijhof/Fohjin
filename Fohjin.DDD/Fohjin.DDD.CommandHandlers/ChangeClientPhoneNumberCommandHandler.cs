using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class ChangeClientPhoneNumberCommandHandler : ICommandHandler<ChangeClientPhoneNumberCommand>
    {
        private readonly IDomainRepository _repository;

        public ChangeClientPhoneNumberCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangeClientPhoneNumberCommand compensatingCommand)
        {
            var client = _repository.GetById<Client>(compensatingCommand.Id);

            client.UpdatePhoneNumber(new PhoneNumber(compensatingCommand.PhoneNumber));

            //_repository.Add(client);
            _repository.Complete();
        }
    }
}