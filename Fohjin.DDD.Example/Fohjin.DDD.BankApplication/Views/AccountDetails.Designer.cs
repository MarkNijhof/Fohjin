using System.Windows.Forms;

namespace Fohjin.DDD.BankApplication.Views
{
    partial class AccountDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            accountToolStripMenuItem = new ToolStripMenuItem();
            changeAccountNameToolStripMenuItem = new ToolStripMenuItem();
            closeAccountToolStripMenuItem = new ToolStripMenuItem();
            transferToolStripMenuItem = new ToolStripMenuItem();
            makeCashMutationToolStripMenuItem = new ToolStripMenuItem();
            makeCashWithdrawalToolStripMenuItem = new ToolStripMenuItem();
            transferMoneyToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            _detailsTab = new TabPage();
            groupBox5 = new GroupBox();
            _ledgers = new ListBox();
            groupBox4 = new GroupBox();
            _balanceLabel = new Label();
            _accountNumberLabel = new Label();
            _accountNameLabel = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            _depositTab = new TabPage();
            _phoneNumberGroupBox = new GroupBox();
            _depositAmount = new TextBox();
            _depositCancelButton = new Button();
            _depositButton = new Button();
            _withdrawalTab = new TabPage();
            groupBox6 = new GroupBox();
            _withdrawalAmount = new TextBox();
            _withdrawalCancelButton = new Button();
            _withdrawalButton = new Button();
            _transferTab = new TabPage();
            groupBox2 = new GroupBox();
            _transferAccounts = new ComboBox();
            label12 = new Label();
            _transferAmount = new TextBox();
            label13 = new Label();
            _transferCancelButton = new Button();
            _transferButton = new Button();
            _nameChangeTab = new TabPage();
            groupBox7 = new GroupBox();
            _accountName = new TextBox();
            _newAccountNameCancelButton = new Button();
            _newAccountNameSaveButton = new Button();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            _detailsTab.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            _depositTab.SuspendLayout();
            _phoneNumberGroupBox.SuspendLayout();
            _withdrawalTab.SuspendLayout();
            groupBox6.SuspendLayout();
            _transferTab.SuspendLayout();
            groupBox2.SuspendLayout();
            _nameChangeTab.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { accountToolStripMenuItem, transferToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(357, 24);
            menuStrip1.TabIndex = 24;
            menuStrip1.Text = "menuStrip1";
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeAccountNameToolStripMenuItem, closeAccountToolStripMenuItem });
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Size = new Size(64, 20);
            accountToolStripMenuItem.Text = "Account";
            // 
            // changeAccountNameToolStripMenuItem
            // 
            changeAccountNameToolStripMenuItem.Name = "changeAccountNameToolStripMenuItem";
            changeAccountNameToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.N;
            changeAccountNameToolStripMenuItem.Size = new Size(233, 22);
            changeAccountNameToolStripMenuItem.Text = "Change account name";
            // 
            // closeAccountToolStripMenuItem
            // 
            closeAccountToolStripMenuItem.Name = "closeAccountToolStripMenuItem";
            closeAccountToolStripMenuItem.Size = new Size(233, 22);
            closeAccountToolStripMenuItem.Text = "Close account";
            // 
            // transferToolStripMenuItem
            // 
            transferToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { makeCashMutationToolStripMenuItem, makeCashWithdrawalToolStripMenuItem, transferMoneyToolStripMenuItem });
            transferToolStripMenuItem.Name = "transferToolStripMenuItem";
            transferToolStripMenuItem.Size = new Size(60, 20);
            transferToolStripMenuItem.Text = "Transfer";
            // 
            // makeCashMutationToolStripMenuItem
            // 
            makeCashMutationToolStripMenuItem.Name = "makeCashMutationToolStripMenuItem";
            makeCashMutationToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.D;
            makeCashMutationToolStripMenuItem.Size = new Size(232, 22);
            makeCashMutationToolStripMenuItem.Text = "Make cash deposit";
            // 
            // makeCashWithdrawalToolStripMenuItem
            // 
            makeCashWithdrawalToolStripMenuItem.Name = "makeCashWithdrawalToolStripMenuItem";
            makeCashWithdrawalToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.W;
            makeCashWithdrawalToolStripMenuItem.Size = new Size(232, 22);
            makeCashWithdrawalToolStripMenuItem.Text = "Make cash withdrawal";
            // 
            // transferMoneyToolStripMenuItem
            // 
            transferMoneyToolStripMenuItem.Name = "transferMoneyToolStripMenuItem";
            transferMoneyToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.T;
            transferMoneyToolStripMenuItem.Size = new Size(232, 22);
            transferMoneyToolStripMenuItem.Text = "Transfer money";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(_detailsTab);
            tabControl1.Controls.Add(_depositTab);
            tabControl1.Controls.Add(_withdrawalTab);
            tabControl1.Controls.Add(_transferTab);
            tabControl1.Controls.Add(_nameChangeTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 24);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(357, 297);
            tabControl1.TabIndex = 25;
            // 
            // _detailsTab
            // 
            _detailsTab.Controls.Add(groupBox5);
            _detailsTab.Controls.Add(groupBox4);
            _detailsTab.Location = new Point(4, 24);
            _detailsTab.Margin = new Padding(4, 3, 4, 3);
            _detailsTab.Name = "_detailsTab";
            _detailsTab.Padding = new Padding(4, 3, 4, 3);
            _detailsTab.Size = new Size(349, 269);
            _detailsTab.TabIndex = 0;
            _detailsTab.Text = "tabPage1";
            _detailsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(_ledgers);
            groupBox5.Location = new Point(7, 119);
            groupBox5.Margin = new Padding(4, 3, 4, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4, 3, 4, 3);
            groupBox5.Size = new Size(332, 137);
            groupBox5.TabIndex = 21;
            groupBox5.TabStop = false;
            groupBox5.Text = "Ledgers";
            // 
            // _ledgers
            // 
            _ledgers.FormattingEnabled = true;
            _ledgers.ItemHeight = 15;
            _ledgers.Location = new Point(7, 22);
            _ledgers.Margin = new Padding(4, 3, 4, 3);
            _ledgers.Name = "_ledgers";
            _ledgers.Size = new Size(318, 109);
            _ledgers.TabIndex = 7;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(_balanceLabel);
            groupBox4.Controls.Add(_accountNumberLabel);
            groupBox4.Controls.Add(_accountNameLabel);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(label8);
            groupBox4.Location = new Point(7, 7);
            groupBox4.Margin = new Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4, 3, 4, 3);
            groupBox4.Size = new Size(332, 105);
            groupBox4.TabIndex = 20;
            groupBox4.TabStop = false;
            groupBox4.Text = "Account details";
            // 
            // _balanceLabel
            // 
            _balanceLabel.AutoSize = true;
            _balanceLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _balanceLabel.Location = new Point(110, 62);
            _balanceLabel.Margin = new Padding(4, 0, 4, 0);
            _balanceLabel.Name = "_balanceLabel";
            _balanceLabel.Size = new Size(39, 13);
            _balanceLabel.TabIndex = 5;
            _balanceLabel.Text = "10.00";
            // 
            // _accountNumberLabel
            // 
            _accountNumberLabel.AutoSize = true;
            _accountNumberLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _accountNumberLabel.Location = new Point(110, 43);
            _accountNumberLabel.Margin = new Padding(4, 0, 4, 0);
            _accountNumberLabel.Name = "_accountNumberLabel";
            _accountNumberLabel.Size = new Size(101, 13);
            _accountNumberLabel.TabIndex = 4;
            _accountNumberLabel.Text = "Account Number";
            // 
            // _accountNameLabel
            // 
            _accountNameLabel.AutoSize = true;
            _accountNameLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            _accountNameLabel.Location = new Point(110, 23);
            _accountNameLabel.Margin = new Padding(4, 0, 4, 0);
            _accountNameLabel.Name = "_accountNameLabel";
            _accountNameLabel.Size = new Size(125, 13);
            _accountNameLabel.TabIndex = 3;
            _accountNameLabel.Text = "Account Name Label";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 62);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 2;
            label6.Text = "Balance:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 43);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(100, 15);
            label7.TabIndex = 1;
            label7.Text = "Account number:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 23);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(42, 15);
            label8.TabIndex = 0;
            label8.Text = "Name:";
            // 
            // _depositTab
            // 
            _depositTab.Controls.Add(_phoneNumberGroupBox);
            _depositTab.Controls.Add(_depositCancelButton);
            _depositTab.Controls.Add(_depositButton);
            _depositTab.Location = new Point(4, 24);
            _depositTab.Margin = new Padding(4, 3, 4, 3);
            _depositTab.Name = "_depositTab";
            _depositTab.Padding = new Padding(4, 3, 4, 3);
            _depositTab.Size = new Size(349, 269);
            _depositTab.TabIndex = 1;
            _depositTab.Text = "tabPage2";
            _depositTab.UseVisualStyleBackColor = true;
            // 
            // _phoneNumberGroupBox
            // 
            _phoneNumberGroupBox.Controls.Add(_depositAmount);
            _phoneNumberGroupBox.Location = new Point(4, 3);
            _phoneNumberGroupBox.Margin = new Padding(4, 3, 4, 3);
            _phoneNumberGroupBox.Name = "_phoneNumberGroupBox";
            _phoneNumberGroupBox.Padding = new Padding(4, 3, 4, 3);
            _phoneNumberGroupBox.Size = new Size(332, 57);
            _phoneNumberGroupBox.TabIndex = 19;
            _phoneNumberGroupBox.TabStop = false;
            _phoneNumberGroupBox.Text = "Specify the amount to be deposit";
            // 
            // _depositAmount
            // 
            _depositAmount.Location = new Point(7, 22);
            _depositAmount.Margin = new Padding(4, 3, 4, 3);
            _depositAmount.Name = "_depositAmount";
            _depositAmount.Size = new Size(318, 23);
            _depositAmount.TabIndex = 0;
            _depositAmount.Text = "0";
            _depositAmount.TextChanged += DepositAmount_TextChanged;
            _depositAmount.KeyPress += Amount_KeyPress;
            // 
            // _depositCancelButton
            // 
            _depositCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _depositCancelButton.Location = new Point(166, 241);
            _depositCancelButton.Margin = new Padding(4, 3, 4, 3);
            _depositCancelButton.Name = "_depositCancelButton";
            _depositCancelButton.Size = new Size(88, 27);
            _depositCancelButton.TabIndex = 2;
            _depositCancelButton.Text = "Cancel";
            _depositCancelButton.UseVisualStyleBackColor = true;
            // 
            // _depositButton
            // 
            _depositButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _depositButton.Location = new Point(260, 241);
            _depositButton.Margin = new Padding(4, 3, 4, 3);
            _depositButton.Name = "_depositButton";
            _depositButton.Size = new Size(88, 27);
            _depositButton.TabIndex = 1;
            _depositButton.Text = "Deposit";
            _depositButton.UseVisualStyleBackColor = true;
            // 
            // _withdrawalTab
            // 
            _withdrawalTab.Controls.Add(groupBox6);
            _withdrawalTab.Controls.Add(_withdrawalCancelButton);
            _withdrawalTab.Controls.Add(_withdrawalButton);
            _withdrawalTab.Location = new Point(4, 24);
            _withdrawalTab.Margin = new Padding(4, 3, 4, 3);
            _withdrawalTab.Name = "_withdrawalTab";
            _withdrawalTab.Size = new Size(349, 269);
            _withdrawalTab.TabIndex = 2;
            _withdrawalTab.Text = "tabPage3";
            _withdrawalTab.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(_withdrawalAmount);
            groupBox6.Location = new Point(4, 3);
            groupBox6.Margin = new Padding(4, 3, 4, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4, 3, 4, 3);
            groupBox6.Size = new Size(332, 57);
            groupBox6.TabIndex = 19;
            groupBox6.TabStop = false;
            groupBox6.Text = "Specify the amount to withdrawal";
            // 
            // _withdrawalAmount
            // 
            _withdrawalAmount.Location = new Point(7, 22);
            _withdrawalAmount.Margin = new Padding(4, 3, 4, 3);
            _withdrawalAmount.Name = "_withdrawalAmount";
            _withdrawalAmount.Size = new Size(318, 23);
            _withdrawalAmount.TabIndex = 0;
            _withdrawalAmount.Text = "0";
            _withdrawalAmount.TextChanged += DepositAmount_TextChanged;
            _withdrawalAmount.KeyPress += Amount_KeyPress;
            // 
            // _withdrawalCancelButton
            // 
            _withdrawalCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _withdrawalCancelButton.Location = new Point(166, 241);
            _withdrawalCancelButton.Margin = new Padding(4, 3, 4, 3);
            _withdrawalCancelButton.Name = "_withdrawalCancelButton";
            _withdrawalCancelButton.Size = new Size(88, 27);
            _withdrawalCancelButton.TabIndex = 2;
            _withdrawalCancelButton.Text = "Cancel";
            _withdrawalCancelButton.UseVisualStyleBackColor = true;
            // 
            // _withdrawalButton
            // 
            _withdrawalButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _withdrawalButton.Location = new Point(260, 241);
            _withdrawalButton.Margin = new Padding(4, 3, 4, 3);
            _withdrawalButton.Name = "_withdrawalButton";
            _withdrawalButton.Size = new Size(88, 27);
            _withdrawalButton.TabIndex = 1;
            _withdrawalButton.Text = "Withdrawal";
            _withdrawalButton.UseVisualStyleBackColor = true;
            // 
            // _transferTab
            // 
            _transferTab.Controls.Add(groupBox2);
            _transferTab.Controls.Add(_transferCancelButton);
            _transferTab.Controls.Add(_transferButton);
            _transferTab.Location = new Point(4, 24);
            _transferTab.Margin = new Padding(4, 3, 4, 3);
            _transferTab.Name = "_transferTab";
            _transferTab.Size = new Size(349, 269);
            _transferTab.TabIndex = 3;
            _transferTab.Text = "tabPage4";
            _transferTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(_transferAccounts);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(_transferAmount);
            groupBox2.Controls.Add(label13);
            groupBox2.Location = new Point(4, 3);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(332, 118);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "Specify the clients new address";
            // 
            // _transferAccounts
            // 
            _transferAccounts.FormattingEnabled = true;
            _transferAccounts.Location = new Point(7, 83);
            _transferAccounts.Margin = new Padding(4, 3, 4, 3);
            _transferAccounts.Name = "_transferAccounts";
            _transferAccounts.Size = new Size(318, 23);
            _transferAccounts.TabIndex = 1;
            _transferAccounts.SelectedIndexChanged += DepositAmount_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(4, 65);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(123, 15);
            label12.TabIndex = 10;
            label12.Text = "Account to transfer to";
            // 
            // _transferAmount
            // 
            _transferAmount.Location = new Point(7, 38);
            _transferAmount.Margin = new Padding(4, 3, 4, 3);
            _transferAmount.Name = "_transferAmount";
            _transferAmount.Size = new Size(318, 23);
            _transferAmount.TabIndex = 0;
            _transferAmount.Text = "0";
            _transferAmount.TextChanged += DepositAmount_TextChanged;
            _transferAmount.KeyPress += Amount_KeyPress;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(4, 18);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(141, 15);
            label13.TabIndex = 8;
            label13.Text = "Amount to be transferred";
            // 
            // _transferCancelButton
            // 
            _transferCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _transferCancelButton.Location = new Point(166, 241);
            _transferCancelButton.Margin = new Padding(4, 3, 4, 3);
            _transferCancelButton.Name = "_transferCancelButton";
            _transferCancelButton.Size = new Size(88, 27);
            _transferCancelButton.TabIndex = 3;
            _transferCancelButton.Text = "Cancel";
            _transferCancelButton.UseVisualStyleBackColor = true;
            // 
            // _transferButton
            // 
            _transferButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _transferButton.Location = new Point(260, 241);
            _transferButton.Margin = new Padding(4, 3, 4, 3);
            _transferButton.Name = "_transferButton";
            _transferButton.Size = new Size(88, 27);
            _transferButton.TabIndex = 2;
            _transferButton.Text = "Transfer";
            _transferButton.UseVisualStyleBackColor = true;
            // 
            // _nameChangeTab
            // 
            _nameChangeTab.Controls.Add(groupBox7);
            _nameChangeTab.Controls.Add(_newAccountNameCancelButton);
            _nameChangeTab.Controls.Add(_newAccountNameSaveButton);
            _nameChangeTab.Location = new Point(4, 24);
            _nameChangeTab.Margin = new Padding(4, 3, 4, 3);
            _nameChangeTab.Name = "_nameChangeTab";
            _nameChangeTab.Size = new Size(349, 269);
            _nameChangeTab.TabIndex = 4;
            _nameChangeTab.Text = "tabPage5";
            _nameChangeTab.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(_accountName);
            groupBox7.Location = new Point(4, 3);
            groupBox7.Margin = new Padding(4, 3, 4, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4, 3, 4, 3);
            groupBox7.Size = new Size(332, 57);
            groupBox7.TabIndex = 20;
            groupBox7.TabStop = false;
            groupBox7.Text = "Specify the new account name";
            // 
            // _accountName
            // 
            _accountName.Location = new Point(7, 22);
            _accountName.Margin = new Padding(4, 3, 4, 3);
            _accountName.Name = "_accountName";
            _accountName.Size = new Size(318, 23);
            _accountName.TabIndex = 0;
            _accountName.TextChanged += DepositAmount_TextChanged;
            // 
            // _newAccountNameCancelButton
            // 
            _newAccountNameCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _newAccountNameCancelButton.Location = new Point(166, 241);
            _newAccountNameCancelButton.Margin = new Padding(4, 3, 4, 3);
            _newAccountNameCancelButton.Name = "_newAccountNameCancelButton";
            _newAccountNameCancelButton.Size = new Size(88, 27);
            _newAccountNameCancelButton.TabIndex = 2;
            _newAccountNameCancelButton.Text = "Cancel";
            _newAccountNameCancelButton.UseVisualStyleBackColor = true;
            // 
            // _newAccountNameSaveButton
            // 
            _newAccountNameSaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _newAccountNameSaveButton.Location = new Point(260, 241);
            _newAccountNameSaveButton.Margin = new Padding(4, 3, 4, 3);
            _newAccountNameSaveButton.Name = "_newAccountNameSaveButton";
            _newAccountNameSaveButton.Size = new Size(88, 27);
            _newAccountNameSaveButton.TabIndex = 1;
            _newAccountNameSaveButton.Text = "Save";
            _newAccountNameSaveButton.UseVisualStyleBackColor = true;
            // 
            // AccountDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(357, 321);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AccountDetails";
            Text = "Account Details";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            _detailsTab.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            _depositTab.ResumeLayout(false);
            _phoneNumberGroupBox.ResumeLayout(false);
            _phoneNumberGroupBox.PerformLayout();
            _withdrawalTab.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            _transferTab.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            _nameChangeTab.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeAccountNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeCashMutationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferMoneyToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _detailsTab;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label _balanceLabel;
        private System.Windows.Forms.Label _accountNumberLabel;
        private System.Windows.Forms.Label _accountNameLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem makeCashWithdrawalToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox _ledgers;
        private System.Windows.Forms.TabPage _withdrawalTab;
        private System.Windows.Forms.TabPage _transferTab;
        private System.Windows.Forms.TabPage _nameChangeTab;
        private System.Windows.Forms.Button _withdrawalCancelButton;
        private System.Windows.Forms.Button _withdrawalButton;
        private System.Windows.Forms.Button _transferCancelButton;
        private System.Windows.Forms.Button _transferButton;
        private System.Windows.Forms.Button _newAccountNameCancelButton;
        private System.Windows.Forms.Button _newAccountNameSaveButton;
        private System.Windows.Forms.TabPage _depositTab;
        private System.Windows.Forms.Button _depositCancelButton;
        private System.Windows.Forms.Button _depositButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox _transferAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox _phoneNumberGroupBox;
        private System.Windows.Forms.TextBox _depositAmount;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox _withdrawalAmount;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox _accountName;
        private System.Windows.Forms.ComboBox _transferAccounts;
    }
}