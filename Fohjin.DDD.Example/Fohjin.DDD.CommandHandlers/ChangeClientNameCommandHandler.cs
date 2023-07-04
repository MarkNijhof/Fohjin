using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers;

public class ChangeClientNameCommandHandler : CommandHandlerBase<ChangeClientNameCommand>
{
    private readonly IDomainRepository<IDomainEvent> _repository;

    public ChangeClientNameCommandHandler(IDomainRepository<IDomainEvent> repository)
    {
        _repository = repository;
    }

    public override Task ExecuteAsync(ChangeClientNameCommand compensatingCommand)
    {
        var client = _repository.GetById<Client>(compensatingCommand.Id);
        client?.UpdateClientName(new ClientName(compensatingCommand.ClientName));
        return Task.CompletedTask;
    }
}