using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers;

public class ChangeClientPhoneNumberCommandHandler : CommandHandlerBase<ChangeClientPhoneNumberCommand>
{
    private readonly IDomainRepository<IDomainEvent> _repository;

    public ChangeClientPhoneNumberCommandHandler(IDomainRepository<IDomainEvent> repository)
    {
        _repository = repository;
    }

    public override Task ExecuteAsync(ChangeClientPhoneNumberCommand compensatingCommand)
    {
        var client = _repository.GetById<Client>(compensatingCommand.Id);
        client?.UpdatePhoneNumber(new PhoneNumber(compensatingCommand.PhoneNumber));
        return Task.CompletedTask;
    }
}