using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Account;

namespace Fohjin.DDD.CommandHandlers
{
    public class CloseAccountCommandHandler : ICommandHandler<CloseAccountCommand>
    {
        private readonly IDomainRepository _repository;

        public CloseAccountCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CloseAccountCommand compensatingCommand)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(compensatingCommand.Id);

            var closedAccount = activeAccount.Close();

            _repository.Save(closedAccount);
            _repository.Save(activeAccount);
        }
    }
}