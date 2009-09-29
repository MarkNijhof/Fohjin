using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities
{
    public interface IActiveAccount : IDomainAggregate
    {
        void Create();
        ClosedAccount Close();
        void Withdrawl(Amount amount);
        void Deposite(Amount amount);
    }
}