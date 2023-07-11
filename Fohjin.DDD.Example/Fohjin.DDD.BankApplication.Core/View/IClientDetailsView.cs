using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IClientDetailsView : IView
    {
        string? ClientName { get; set; }
        string? Street { get; set; }
        string? StreetNumber { get; set; }
        string? PostalCode { get; set; }
        string? City { get; set; }
        string? PhoneNumber { get; set; }
        string? NewAccountName { get; set; }

        string? ClientNameLabel { set; }
        string? AddressLine1Label { set; }
        string? AddressLine2Label { set; }
        string? PhoneNumberLabel { set; }

        IEnumerable<AccountReport>? Accounts { get; set; }
        AccountReport? GetSelectedAccount();

        IEnumerable<ClosedAccountReport>? ClosedAccounts { get; set; }
        ClosedAccountReport? GetSelectedClosedAccount();

        void EnableAddNewAccountMenu();
        void DisableAddNewAccountMenu();
        void EnableClientHasMovedMenu();
        void DisableClientHasMovedMenu();
        void EnableNameChangedMenu();
        void DisableNameChangedMenu();
        void EnablePhoneNumberChangedMenu();
        void DisablePhoneNumberChangedMenu();

        void EnableSaveButton();
        void DisableSaveButton();
        void EnableOverviewPanel();
        void EnableAddressPanel();
        void EnableClientNamePanel();
        void EnablePhoneNumberPanel();
        void EnableAddNewAccountPanel();

        event EventAction OnOpenSelectedAccount;
        event EventAction OnFormElementGotChanged;
        event EventAction OnCancel;
        event EventAction OnSaveNewClientName;
        event EventAction OnSaveNewPhoneNumber;
        event EventAction OnSaveNewAddress;
        event EventAction OnInitiateClientHasMoved;
        event EventAction OnInitiateClientNameChange;
        event EventAction OnInitiateClientPhoneNumberChanged;
        event EventAction OnInitiateOpenNewAccount;
        event EventAction OnCreateNewAccount;
    }
}