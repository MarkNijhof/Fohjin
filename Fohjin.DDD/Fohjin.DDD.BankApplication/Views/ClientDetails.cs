using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class ClientDetails : Form, IClientDetailsView
    {
        public ClientDetails()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            RegisterCLientEvents();
        }

        public event Action OnOpenSelectedAccount;
        public event Action OnOpenSelectedClosedAccount;
        public event Action OnFormElementGotChanged;
        public event Action OnCancel;
        public event Action OnSaveNewClientName;
        public event Action OnSaveNewPhoneNumber;
        public event Action OnSaveNewAddress;
        public event Action OnInitiateClientHasMoved;
        public event Action OnInitiateClientNameChange;
        public event Action OnInitiateClientPhoneNumberChanged;
        public event Action OnInitiateAddNewAccount;
        public event Action OnCreateNewAccount;
        public event Action OnRefresh;

        private void RegisterCLientEvents()
        {
            nameChangedToolStripMenuItem.Click += (e, s) => OnInitiateClientNameChange();
            hasMovedToolStripMenuItem.Click += (e, s) => OnInitiateClientHasMoved();
            changedHisPhoneNumberToolStripMenuItem.Click += (e, s) => OnInitiateClientPhoneNumberChanged();
            addNewAccountToolStripMenuItem.Click += (e, s) => OnInitiateAddNewAccount();
            _newAccountCreateButton.Click += (e, s) => OnCreateNewAccount();
            _newAccountCancelButton.Click += (e, s) => OnCancel();
            _clientNameSaveButton.Click += (e, s) => OnSaveNewClientName();
            _clientNameCancelButton.Click += (e, s) => OnCancel();
            _accounts.DoubleClick += (e, s) => OnOpenSelectedAccount();
            _closedAccounts.DoubleClick += (e, s) => OnOpenSelectedClosedAccount();
            _addressCancelButton.Click += (e, s) => OnCancel();
            _addressSaveButton.Click += (e, s) => OnSaveNewAddress();
            _phoneNumberCancelButton.Click += (e, s) => OnCancel();
            _phoneNumberSaveButton.Click += (e, s) => OnSaveNewPhoneNumber();
            _refreshButton.Click += (e, s) => OnRefresh();
        }

        public string ClientName
        {
            get { return _clientName.Text; }
            set { _clientName.Text = value; }
        }

        public string Street
        {
            get { return _street.Text; }
            set { _street.Text = value; }
        }

        public string StreetNumber
        {
            get { return _streetNumber.Text; }
            set { _streetNumber.Text = value; }
        }

        public string PostalCode
        {
            get { return _postalCode.Text; }
            set { _postalCode.Text = value; }
        }

        public string City
        {
            get { return _city.Text; }
            set { _city.Text = value; }
        }

        public IEnumerable<AccountReport> Accounts
        {
            get { return (IEnumerable<AccountReport>)_accounts.DataSource; }
            set { _accounts.DataSource = value; }
        }

        public IEnumerable<ClosedAccountReport> ClosedAccounts
        {
            get { return (IEnumerable<ClosedAccountReport>)_closedAccounts.DataSource; }
            set { _closedAccounts.DataSource = value; }
        }

        public AccountReport GetSelectedAccount()
        {
            return (AccountReport)_accounts.SelectedItem;
        }

        public ClosedAccountReport GetSelectedClosedAccount()
        {
            return (ClosedAccountReport)_closedAccounts.SelectedItem;
        }

        public string PhoneNumber
        {
            get { return _phoneNumber.Text; }
            set { _phoneNumber.Text = value; }
        }

        public string NewAccountName
        {
            get { return _newAccountName.Text; }
            set { _newAccountName.Text = value; }
        }

        public string ClientNameLabel
        {
            set { _clientNameLabel.Text = value; }
        }

        public string AddressLine1Label
        {
            set { _addressLine1Label.Text = value; }
        }

        public string AddressLine2Label
        {
            set { _addressLine2Label.Text = value; }
        }

        public string PhoneNumberLabel
        {
            set { _phoneNumberLabel.Text = value; }
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
        }

        public void EnablePhoneNumberPanel()
        {
            tabControl1.SelectedIndex = 2;
        }

        public void EnableClientNamePanel()
        {
            tabControl1.SelectedIndex = 3;
        }

        public void EnableAddNewAccountPanel()
        {
            tabControl1.SelectedIndex = 4;
        }

        private void _client_Changed(object sender, EventArgs e)
        {
            if (OnFormElementGotChanged != null)
                OnFormElementGotChanged();
        }
    }
}