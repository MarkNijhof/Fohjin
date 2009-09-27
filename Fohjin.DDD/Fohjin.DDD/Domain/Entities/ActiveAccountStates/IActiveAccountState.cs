using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities.ActiveAccountStates
{
    public interface IActiveAccountState
    {
        void Create();
        ClosedAccount Close();
        void Withdrawl(Amount amount);
        void Deposite(Amount amount);
    }
}