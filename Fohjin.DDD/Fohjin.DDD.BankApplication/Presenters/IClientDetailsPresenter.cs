using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IClientDetailsPresenter : IPresenter
    {
        void SetClient(Client client);
        void OpenSelectedAccount();
        void OpenSelectedClosedAccount();
        void FormElementGotChanged();
        void Cancel();
        void SaveNewClientName();
        void SaveNewPhoneNumber();
        void SaveNewAddress();
        void InitiateClientHasMoved();
        void InitiateClientNameChange();
        void InitiateClientPhoneNumberChanged();
        void InitiateAddNewAccount();
        void CreateNewAccount();
        void Refresh();
    }
}