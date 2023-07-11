using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class AccountDetails : ViewFormBase, IAccountDetailsView
    {
        public AccountDetails()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            RegisterClientEvents();
        }

        public event EventAction? OnCloseTheAccount;
        public event EventAction? OnFormElementGotChanged;
        public event EventAction? OnCancel;
        public event EventAction? OnInitiateAccountNameChange;
        public event EventAction? OnInitiateMoneyDeposit;
        public event EventAction? OnInitiateMoneyWithdrawal;
        public event EventAction? OnInitiateMoneyTransfer;
        public event EventAction? OnChangeAccountName;
        public event EventAction? OnDepositMoney;
        public event EventAction? OnWithdrawalMoney;
        public event EventAction? OnTransferMoney;

        private void RegisterClientEvents()
        {
            changeAccountNameToolStripMenuItem.Click += (s, e) => OnInitiateAccountNameChange?.Invoke();
            closeAccountToolStripMenuItem.Click += (s, e) => OnCloseTheAccount?.Invoke();
            makeCashMutationToolStripMenuItem.Click += (s, e) => OnInitiateMoneyDeposit?.Invoke();
            makeCashWithdrawalToolStripMenuItem.Click += (s, e) => OnInitiateMoneyWithdrawal?.Invoke();
            transferMoneyToolStripMenuItem.Click += (s, e) => OnInitiateMoneyTransfer?.Invoke();
            _depositCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _depositButton.Click += (s, e) => OnDepositMoney?.Invoke();
            _withdrawalCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _withdrawalButton.Click += (s, e) => OnWithdrawalMoney?.Invoke();
            _transferCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _transferButton.Click += (s, e) => OnTransferMoney?.Invoke();
            _newAccountNameCancelButton.Click += (s, e) => OnCancel?.Invoke();
            _newAccountNameSaveButton.Click += (s, e) => OnChangeAccountName?.Invoke();
        }

        public string? AccountNameLabel
        {
            set { _accountNameLabel.Text = value; }
        }

        public string? AccountNumberLabel
        {
            set { _accountNumberLabel.Text = value; }
        }

        public IEnumerable<LedgerReport>? Ledgers
        {
            get => _ledgers.DataSource as IEnumerable<LedgerReport>;
            set => _ledgers.DataSource = value;
        }

        public IEnumerable<AccountReport>? TransferAccounts
        {
            get => _transferAccounts.DataSource as IEnumerable<AccountReport>;
            set => _transferAccounts.DataSource = value;
        }

        public void EnableDetailsPanel()
        {
            tabControl1.SelectedIndex = 0;
        }

        public void EnableDepositPanel()
        {
            tabControl1.SelectedIndex = 1;
            _depositAmount.Focus();
        }

        public void EnableWithdrawalPanel()
        {
            tabControl1.SelectedIndex = 2;
            _withdrawalAmount.Focus();
        }

        public void EnableTransferPanel()
        {
            tabControl1.SelectedIndex = 3;
            _transferAmount.Focus();
        }

        public void EnableAccountNameChangePanel()
        {
            tabControl1.SelectedIndex = 4;
            _accountName.Focus();
        }

        public void EnableSaveButton()
        {
            _depositButton.Enabled = true;
            _withdrawalButton.Enabled = true;
            _transferButton.Enabled = true;
            _newAccountNameSaveButton.Enabled = true;
        }

        public void DisableSaveButton()
        {
            _depositButton.Enabled = false;
            _withdrawalButton.Enabled = false;
            _transferButton.Enabled = false;
            _newAccountNameSaveButton.Enabled = false;
        }

        public void EnableMenuButtons()
        {
            changeAccountNameToolStripMenuItem.Enabled = true;
            closeAccountToolStripMenuItem.Enabled = true;
            makeCashWithdrawalToolStripMenuItem.Enabled = true;
            makeCashMutationToolStripMenuItem.Enabled = true;
            transferMoneyToolStripMenuItem.Enabled = true;
        }

        public void DisableMenuButtons()
        {
            changeAccountNameToolStripMenuItem.Enabled = false;
            closeAccountToolStripMenuItem.Enabled = false;
            makeCashWithdrawalToolStripMenuItem.Enabled = false;
            makeCashMutationToolStripMenuItem.Enabled = false;
            transferMoneyToolStripMenuItem.Enabled = false;
        }

        public AccountReport GetSelectedTransferAccount()
        {
            return (AccountReport)_transferAccounts.SelectedItem;
        }

        public decimal DepositAmount
        {
            get { return _depositAmount.Text.Trim() == "," ? 0 : Convert.ToDecimal(_depositAmount.Text.Trim()); }
            set { _depositAmount.Text = value.ToString(); }
        }

        public decimal WithdrawalAmount
        {
            get { return _withdrawalAmount.Text.Trim() == "," ? 0 : Convert.ToDecimal(_withdrawalAmount.Text.Trim()); }
            set { _withdrawalAmount.Text = value.ToString(); }
        }

        public decimal TransferAmount
        {
            get { return _transferAmount.Text.Trim() == "," ? 0 : Convert.ToDecimal(_transferAmount.Text.Trim()); }
            set { _transferAmount.Text = value.ToString(); }
        }

        public decimal BalanceLabel
        {
            set { _balanceLabel.Text = value.ToString(); }
        }

        public string? AccountName
        {
            get { return _accountName.Text; }
            set { _accountName.Text = value; }
        }

        private void Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Decimal.TryParse(e.KeyChar.ToString(), out _);
        }

        private void DepositAmount_TextChanged(object sender, EventArgs e)
        {
            OnFormElementGotChanged?.Invoke();
        }
    }
}