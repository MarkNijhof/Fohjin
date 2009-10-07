using Fohjin.DDD.Commands.Client;
using Fohjin.DDD.Domain.Entities;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers.Client
{
    public class ClientHasMovedCommandHandler : ICommandHandler<ClientHasMovedCommand>
    {
        private readonly IRepository<ActiveAccount> _repository;

        public ClientHasMovedCommandHandler(IRepository<ActiveAccount> repository)
        {
            _repository = repository;
        }

        public void Execute(ClientHasMovedCommand command)
        {
            var activeAccount = _repository.GetById(command.Id);

            activeAccount.Withdrawl(10);

            _repository.Save(activeAccount);
        }
    }
}