using System.Linq;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.CommandHandlers
{
    public class TransferMoneyFromAnOtherAccountCommandHandler : ICommandHandler<TransferMoneyFromAnOtherAccountCommand>
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IReportingRepository _reportingRepository;

        public TransferMoneyFromAnOtherAccountCommandHandler(IDomainRepository domainRepository, IReportingRepository reportingRepository)
        {
            _domainRepository = domainRepository;
            _reportingRepository = reportingRepository;
        }

        public void Execute(TransferMoneyFromAnOtherAccountCommand command)
        {
            var account = _reportingRepository.GetByExample<Account>(new { command.AccountNumber }).First();

            var activeAccount = _domainRepository.GetById<ActiveAccount>(account.Id);

            activeAccount.ReceiveTransferFrom(new AccountNumber(command.AccountNumber), new Amount(command.Amount));

            _domainRepository.Save(activeAccount);
        }
    }
}