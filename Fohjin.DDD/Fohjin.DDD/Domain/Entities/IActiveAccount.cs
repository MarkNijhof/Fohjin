using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities
{
    public interface IActiveAccount
    {
        void Create();
        ClosedAccount Close();
        void Withdrawl(Amount amount);
        void Deposite(Amount amount);
    }
}