namespace Fohjin.DDD.Domain.Client
{
    public interface IBankCard
    {
        void BankCardIsReportedStolen();
        void ClientCancelsBankCard();
    }
}