using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.CommandHandlers
{
    public class DepositeCashCommandHandler : ICommandHandler<DepositeCashCommand>
    {
        private readonly IDomainRepository _repository;

        public DepositeCashCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(DepositeCashCommand command)
        {
            var activeAccount = _repository.GetById<ActiveAccount>(command.Id);

            activeAccount.Deposite(new Amount(command.Amount));

            _repository.Save(activeAccount);
        }
    }
}