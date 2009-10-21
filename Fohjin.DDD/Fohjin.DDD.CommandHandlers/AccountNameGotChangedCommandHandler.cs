using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class AccountNameGotChangedCommandHandler : ICommandHandler<AccountNameGotChangedCommand>
    {
        private readonly IDomainRepository _repository;

        public AccountNameGotChangedCommandHandler(IDomainRepository repository)
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