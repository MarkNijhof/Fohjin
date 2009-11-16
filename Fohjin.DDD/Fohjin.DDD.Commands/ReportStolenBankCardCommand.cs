using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class ReportStolenBankCardCommand : Command
    {
        public Guid BankCardId { get; private set; }

        public ReportStolenBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}