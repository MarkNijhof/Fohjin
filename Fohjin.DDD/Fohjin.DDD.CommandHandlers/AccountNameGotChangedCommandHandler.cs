using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class AccountNameGotChangedCommandHandler : ICommandHandler<AccountNameGotChangedCommand>
    {
        private readonly IRepository _repository;

        public AccountNameGotChangedCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute(AccountNameGotChangedCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.ChangeAccountName(new AccountName(command.AccountName));

            _repository.Save(activeAccount);
        }
    }
}