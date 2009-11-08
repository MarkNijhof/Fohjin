using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.CommandHandlers
{
    public class ChangeAccountNameCommandHandler : ICommandHandler<ChangeAccountNameCommand>
    {
        private readonly IDomainRepository _repository;

        public ChangeAccountNameCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangeAccountNameCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            activeAccount.ChangeAccountName(new AccountName(compensatingCommand.AccountName));

            _repository.Complete();
        }
    }
}