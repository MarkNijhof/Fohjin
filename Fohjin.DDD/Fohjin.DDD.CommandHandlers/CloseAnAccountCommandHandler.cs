using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class CloseAccountCommandHandler : ICommandHandler<CloseAccountCommand>
    {
        private readonly IRepository<ActiveAccount> _repository;

        public CloseAccountCommandHandler(IRepository<ActiveAccount> repository)
        {
            _repository = repository;
        }

        public void Execute(CloseAccountCommand command)
        {
            var activeAccount = _repository.GetById(command.Id);

            activeAccount.Close();

            _repository.Save(activeAccount);
        }
    }
}