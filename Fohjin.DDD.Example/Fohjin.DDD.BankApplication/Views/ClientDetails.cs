using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class ClientDetails : ViewFormBase, IClientDetailsView
    {
        public ClientDetails()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            RegisterClientEvents();
        }

        public event EventAction? OnOpenSelectedAccount;
        public event EventAction? OnFormElementGotChanged;
        public event EventAction? OnCancel;
        public event EventAction? OnSaveNewClientName;
        public event EventAction? OnSaveNewPhoneNumber;
        public event EventAction? OnSaveNewAddress;
        public event EventAction? OnInitiateClientHasMoved;
        public event EventAction? OnInitiateClientNameChange;
        public event EventAction? OnInitiateClientPhoneNumberChanged;
        public event EventAction? OnInitiateOpenNewAccount;
        public event EventAction? OnCreateNewAccount;

        private void RegisterClientEvents()
        {
            nameChangedToolStripMenuItem.Click += (s, e) => OnInitiateClientNameChange?.Invoke();
            hasMovedToolStripMenuItem.Click += (s, e) => OnInitiateClientHasMoved?.Invoke();
            changedHisPhoneNumberToolStripMenuItem.Click += (s, e) => OnInitiateClientPhoneNumberChanged?.Invoke();
            addNewAccountToolStripMenuItem.Click += (s, e) => OnInitiateOpenNewAccount?.Invoke();
            _newAccountCreateButton.Click += (s, e) => OnCreateNewAccount?.Invoke();
            _newAccountCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _clientNameSaveButton.Click += (s, e) => OnSaveNewClientName?.Invoke();
            _clientNameCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _accounts.DoubleClick += (s, e) => OnOpenSelectedAccount?.Invoke();
            _addressCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _addressSaveButton.Click += (s, e) => OnSaveNewAddress?.Invoke();
            _phoneNumberCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _phoneNumberSaveButton.Click += (s, e) => OnSaveNewPhoneNumber?.Invoke();
        }

        public string? ClientName
        {
            get { return _clientName.Text; }
            set { _clientName.Text = value; }
        }

        public string? Street
        {
            get { return _street.Text; }
            set { _street.Text = value; }
        }

        public string? StreetNumber
        {
            get { return _streetNumber.Text; }
            set { _streetNumber.Text = value; }
        }

        public string? PostalCode
        {
            get { return _postalCode.Text; }
            set { _postalCode.Text = value; }
        }

        public string? City
        {
            get { return _city.Text; }
            set { _city.Text = value; }
        }

        public IEnumerable<AccountReport>? Accounts
        {
            get { return _accounts.DataSource as IEnumerable<AccountReport>; }
            set { _accounts.DataSource = value; }
        }

        public IEnumerable<ClosedAccountReport>? ClosedAccounts
        {
            get { return _closedAccounts.DataSource as IEnumerable<ClosedAccountReport>; }
            set { _closedAccounts.DataSource = value; }
        }

        public AccountReport? GetSelectedAccount() =>
            _accounts.SelectedItem as AccountReport;

        public ClosedAccountReport? GetSelectedClosedAccount() =>
            _closedAccounts.SelectedItem as ClosedAccountReport;

        public string? PhoneNumber
        {
            get => _phoneNumber.Text;
            set => _phoneNumber.Text = value;
        }

        public string? NewAccountName
        {
            get => _newAccountName.Text;
            set => _newAccountName.Text = value;
        }

        public string? ClientNameLabel
        {
            set => _clientNameLabel.Text = value;
        }

        public string? AddressLine1Label
        {
            set => _addressLine1Label.Text = value;
        }

        public string? AddressLine2Label
        {
            set => _addressLine2Label.Text = value;
        }

        public string? PhoneNumberLabel
        {
            set => _phoneNumberLabel.Text = value;
        }

        public void DisableAddNewAccountMenu()
        {
            addNewAccountToolStripMenuItem.Enabled = false;
        }

        public void EnableClientHasMovedMenu()
        {
            hasMovedToolStripMenuItem.Enabled = true;
        }

        public void DisableClientHasMovedMenu()
        {
            hasMovedToolStripMenuItem.Enabled = false;
        }

        public void EnableNameChangedMenu()
        {
            nameChangedToolStripMenuItem.Enabled = true;
        }

        public void DisableNameChangedMenu()
        {
            nameChangedToolStripMenuItem.Enabled = false;
        }

        public void EnablePhoneNumberChangedMenu()
        {
            changedHisPhoneNumberToolStripMenuItem.Enabled = true;
        }

        public void DisablePhoneNumberChangedMenu()
        {
            changedHisPhoneNumberToolStripMenuItem.Enabled = false;
        }

        public void EnableAddNewAccountMenu()
        {
            addNewAccountToolStripMenuItem.Enabled = true;
        }

        public void EnableSaveButton()
        {
            _addressSaveButton.Enabled = true;
            _phoneNumberSaveButton.Enabled = true;
            _clientNameSaveButton.Enabled = true;
            _newAccountCreateButton.Enabled = true;
        }

        public void DisableSaveButton()
        {
            _addressSaveButton.Enabled = false;
            _phoneNumberSaveButton.Enabled = false;
            _clientNameSaveButton.Enabled = false;
            _newAccountCreateButton.Enabled = false;
        }

        public void EnableOverviewPanel()
        {
            tabControl1.SelectedIndex = 0;
        }

        public void EnableAddressPanel()
        {
            tabControl1.SelectedIndex = 1;
            _street.Focus();
        }

        public void EnablePhoneNumberPanel()
        {
            tabControl1.SelectedIndex = 2;
            _phoneNumber.Focus();
        }

        public void EnableClientNamePanel()
        {
            tabControl1.SelectedIndex = 3;
            _clientName.Focus();
        }

        public void EnableAddNewAccountPanel()
        {
            tabControl1.SelectedIndex = 4;
            _newAccountName.Focus();
        }

        private void ClientChanged(object sender, EventArgs e) =>
            OnFormElementGotChanged?.Invoke();
    }
}