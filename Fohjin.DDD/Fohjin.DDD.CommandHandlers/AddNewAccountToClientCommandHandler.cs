using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class AddNewAccountToClientCommandHandler : ICommandHandler<AddNewAccountToClientCommand>
    {
        private readonly IRepository _repository;

        public AddNewAccountToClientCommandHandler(IRepository activeAccountRepository)
        {
            _repository = activeAccountRepository;
        }

        public void Execute(AddNewAccountToClientCommand command)
        {
            //var activeAccount = new ActiveAccount();
            //activeAccount.Create(command.AccountId, new AccountName(command.AccountName));

            //var client = _repository.GetById<Client>(command.Id);
            //client.AddAccount(command.AccountId);

            //_repository.Save(activeAccount);
            //_repository.Save(client);
        }
    }
}