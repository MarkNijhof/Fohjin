using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Account;

namespace Fohjin.DDD.CommandHandlers
{
    public class ChangeAccountNameCommandHandler : ICommandHandler<ChangeAccountNameCommand>
    {
        private readonly IDomainRepository _repository;

        public ChangeAccountNameCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangeAccountNameCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.ChangeAccountName(new AccountName(command.AccountName));

            _repository.Save(activeAccount);
        }
    }
}