using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class CloseAccountCommandHandler : CommandHandlerBase<CloseAccountCommand>
    {
        private readonly IDomainRepository<IDomainEvent> _repository;

        public CloseAccountCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            _repository = repository;
        }

        public override Task ExecuteAsync(CloseAccountCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            var closedAccount = activeAccount?.Close();

            if (closedAccount != null)
                _repository.Add(closedAccount);
            return Task.CompletedTask;
        }
    }
}