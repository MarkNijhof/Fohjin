using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class CloseAccountCommandHandler : ICommandHandler<CloseAccountCommand>
    {
        private readonly IRepository _repository;

        public CloseAccountCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CloseAccountCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.Close();

            _repository.Save(activeAccount);
        }
    }
}