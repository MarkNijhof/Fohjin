using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class ChangeClientPhoneNumberCommandHandler : ICommandHandler<ChangeClientPhoneNumberCommand>
    {
        private readonly IDomainRepository _repository;

        public ChangeClientPhoneNumberCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangeClientPhoneNumberCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);

            client.UpdatePhoneNumber(new PhoneNumber(command.PhoneNumber));

            _repository.Save(client);
        }
    }
}