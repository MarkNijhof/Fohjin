using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class AddNewAccountToClientCommandHandler : ICommandHandler<AddNewAccountToClientCommand>
    {
        private readonly IRepository<ActiveAccount> _activeAccountRepository;
        private readonly IRepository<Client> _clientRepository;

        public AddNewAccountToClientCommandHandler(IRepository<ActiveAccount> activeAccountRepository, IRepository<Client> clientRepository)
        {
            _activeAccountRepository = activeAccountRepository;
            _clientRepository = clientRepository;
        }

        public void Execute(AddNewAccountToClientCommand command)
        {
            var activeAccount = new ActiveAccount();
            activeAccount.Create(command.AccountId, new AccountName(command.AccountName));

            var client = _clientRepository.GetById(command.Id);
            client.AddAccount(command.AccountId);

            _activeAccountRepository.Save(activeAccount);
            _clientRepository.Save(client);
        }
    }
}