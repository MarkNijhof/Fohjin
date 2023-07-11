using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IClientDetailsPresenter : IPresenter
    {
        void SetClient(ClientReport? clientReport);
    }
}