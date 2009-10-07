using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IClientDetailsPresenter : IPresenter
    {
        void SetClient(ClientDto clientDto);
        void OpenSelectedAccount();
        void CreateNewAccountAndAttachToClient();
        void SaveClientChanges();
    }
}