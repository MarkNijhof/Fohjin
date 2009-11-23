using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IClientDetailsPresenter : IPresenter
    {
        void SetClient(ClientReport clientReport);
    }
}