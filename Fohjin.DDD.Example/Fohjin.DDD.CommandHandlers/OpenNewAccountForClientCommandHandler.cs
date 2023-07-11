using Fohjin.DDD.Commands;
using Fohjin.DDD.Common;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers;

public class OpenNewAccountForClientCommandHandler : CommandHandlerBase<OpenNewAccountForClientCommand>
{
    private readonly IDomainRepository<IDomainEvent> _repository;
    private readonly ISystemHash _systemHash;

    public OpenNewAccountForClientCommandHandler(
        IDomainRepository<IDomainEvent> repository,
        ISystemHash systemHash
        )
    {
        _repository = repository;
        _systemHash = systemHash;
    }

    public override Task ExecuteAsync(OpenNewAccountForClientCommand compensatingCommand)
    {
        var client = _repository.GetById<Client>(compensatingCommand.Id);
        var activeAccount = client?.CreateNewAccount(
            compensatingCommand.AccountName, 
            _systemHash.Hash(compensatingCommand.AccountName)
            );

        if (activeAccount != null)
            _repository.Add(activeAccount);

        return Task.CompletedTask;
    }
}