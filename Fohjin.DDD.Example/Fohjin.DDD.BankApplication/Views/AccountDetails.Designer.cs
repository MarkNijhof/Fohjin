﻿using System.Windows.Forms;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAccountNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeCashMutationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeCashWithdrawalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferMoneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._detailsTab = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this._ledgers = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._balanceLabel = new System.Windows.Forms.Label();
            this._accountNumberLabel = new System.Windows.Forms.Label();
            this._accountNameLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._depositTab = new System.Windows.Forms.TabPage();
            this._phoneNumberGroupBox = new System.Windows.Forms.GroupBox();
            this._depositAmount = new System.Windows.Forms.TextBox();
            this._depositCancelButton = new System.Windows.Forms.Button();
            this._depositButton = new System.Windows.Forms.Button();
            this._withdrawalTab = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this._withdrawalAmount = new System.Windows.Forms.TextBox();
            this._withdrawalCancelButton = new System.Windows.Forms.Button();
            this._withdrawalButton = new System.Windows.Forms.Button();
            this._transferTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._transferAccounts = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this._transferAmount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this._transferCancelButton = new System.Windows.Forms.Button();
            this._transferButton = new System.Windows.Forms.Button();
            this._nameChangeTab = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this._accountName = new System.Windows.Forms.TextBox();
            this._newAccountNameCancelButton = new System.Windows.Forms.Button();
            this._newAccountNameSaveButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this._detailsTab.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this._depositTab.SuspendLayout();
            this._phoneNumberGroupBox.SuspendLayout();
            this._withdrawalTab.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this._transferTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this._nameChangeTab.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountToolStripMenuItem,
            this.transferToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(306, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeAccountNameToolStripMenuItem,
            this.closeAccountToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // changeAccountNameToolStripMenuItem
            // 
            this.changeAccountNameToolStripMenuItem.Name = "changeAccountNameToolStripMenuItem";
            this.changeAccountNameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.changeAccountNameToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.changeAccountNameToolStripMenuItem.Text = "Change account name";
            // 
            // closeAccountToolStripMenuItem
            // 
            this.closeAccountToolStripMenuItem.Name = "closeAccountToolStripMenuItem";
            this.closeAccountToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.closeAccountToolStripMenuItem.Text = "Close account";
            // 
            // transferToolStripMenuItem
            // 
            this.transferToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeCashMutationToolStripMenuItem,
            this.makeCashWithdrawalToolStripMenuItem,
            this.transferMoneyToolStripMenuItem});
            this.transferToolStripMenuItem.Name = "transferToolStripMenuItem";
            this.transferToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.transferToolStripMenuItem.Text = "Transfer";
            // 
            // makeCashMutationToolStripMenuItem
            // 
            this.makeCashMutationToolStripMenuItem.Name = "makeCashMutationToolStripMenuItem";
            this.makeCashMutationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.makeCashMutationToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.makeCashMutationToolStripMenuItem.Text = "Make cash deposit";
            // 
            // makeCashWithdrawalToolStripMenuItem
            // 
            this.makeCashWithdrawalToolStripMenuItem.Name = "makeCashWithdrawalToolStripMenuItem";
            this.makeCashWithdrawalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.makeCashWithdrawalToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.makeCashWithdrawalToolStripMenuItem.Text = "Make cash withdrawal";
            // 
            // transferMoneyToolStripMenuItem
            // 
            this.transferMoneyToolStripMenuItem.Name = "transferMoneyToolStripMenuItem";
            this.transferMoneyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.transferMoneyToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.transferMoneyToolStripMenuItem.Text = "Transfer money";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._detailsTab);
            this.tabControl1.Controls.Add(this._depositTab);
            this.tabControl1.Controls.Add(this._withdrawalTab);
            this.tabControl1.Controls.Add(this._transferTab);
            this.tabControl1.Controls.Add(this._nameChangeTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(306, 254);
            this.tabControl1.TabIndex = 25;
            // 
            // _detailsTab
            // 
            this._detailsTab.Controls.Add(this.groupBox5);
            this._detailsTab.Controls.Add(this.groupBox4);
            this._detailsTab.Location = new System.Drawing.Point(4, 22);
            this._detailsTab.Name = "_detailsTab";
            this._detailsTab.Padding = new System.Windows.Forms.Padding(3);
            this._detailsTab.Size = new System.Drawing.Size(298, 228);
            this._detailsTab.TabIndex = 0;
            this._detailsTab.Text = "tabPage1";
            this._detailsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this._ledgers);
            this.groupBox5.Location = new System.Drawing.Point(6, 103);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(285, 119);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ledgers";
            // 
            // _ledgers
            // 
            this._ledgers.FormattingEnabled = true;
            this._ledgers.Location = new System.Drawing.Point(6, 19);
            this._ledgers.Name = "_ledgers";
            this._ledgers.Size = new System.Drawing.Size(273, 95);
            this._ledgers.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._balanceLabel);
            this.groupBox4.Controls.Add(this._accountNumberLabel);
            this.groupBox4.Controls.Add(this._accountNameLabel);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(285, 91);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Account details";
            // 
            // _balanceLabel
            // 
            this._balanceLabel.AutoSize = true;
            this._balanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._balanceLabel.Location = new System.Drawing.Point(94, 54);
            this._balanceLabel.Name = "_balanceLabel";
            this._balanceLabel.Size = new System.Drawing.Size(39, 13);
            this._balanceLabel.TabIndex = 5;
            this._balanceLabel.Text = "10.00";
            // 
            // _accountNumberLabel
            // 
            this._accountNumberLabel.AutoSize = true;
            this._accountNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._accountNumberLabel.Location = new System.Drawing.Point(94, 37);
            this._accountNumberLabel.Name = "_accountNumberLabel";
            this._accountNumberLabel.Size = new System.Drawing.Size(101, 13);
            this._accountNumberLabel.TabIndex = 4;
            this._accountNumberLabel.Text = "Account Number";
            // 
            // _accountNameLabel
            // 
            this._accountNameLabel.AutoSize = true;
            this._accountNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._accountNameLabel.Location = new System.Drawing.Point(94, 20);
            this._accountNameLabel.Name = "_accountNameLabel";
            this._accountNameLabel.Size = new System.Drawing.Size(125, 13);
            this._accountNameLabel.TabIndex = 3;
            this._accountNameLabel.Text = "Account Name Label";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Balance:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Account number:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Name:";
            // 
            // _depositTab
            // 
            this._depositTab.Controls.Add(this._phoneNumberGroupBox);
            this._depositTab.Controls.Add(this._depositCancelButton);
            this._depositTab.Controls.Add(this._depositButton);
            this._depositTab.Location = new System.Drawing.Point(4, 22);
            this._depositTab.Name = "_depositTab";
            this._depositTab.Padding = new System.Windows.Forms.Padding(3);
            this._depositTab.Size = new System.Drawing.Size(298, 228);
            this._depositTab.TabIndex = 1;
            this._depositTab.Text = "tabPage2";
            this._depositTab.UseVisualStyleBackColor = true;
            // 
            // _phoneNumberGroupBox
            // 
            this._phoneNumberGroupBox.Controls.Add(this._depositAmount);
            this._phoneNumberGroupBox.Location = new System.Drawing.Point(3, 3);
            this._phoneNumberGroupBox.Name = "_phoneNumberGroupBox";
            this._phoneNumberGroupBox.Size = new System.Drawing.Size(285, 49);
            this._phoneNumberGroupBox.TabIndex = 19;
            this._phoneNumberGroupBox.TabStop = false;
            this._phoneNumberGroupBox.Text = "Specify the amount to be deposit";
            // 
            // _depositAmount
            // 
            this._depositAmount.Location = new System.Drawing.Point(6, 19);
            this._depositAmount.Name = "_depositAmount";
            this._depositAmount.Size = new System.Drawing.Size(273, 20);
            this._depositAmount.TabIndex = 0;
            this._depositAmount.Text = "0";
            this._depositAmount.TextChanged += new System.EventHandler(this._depositAmount_TextChanged);
            this._depositAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._amount_KeyPress);
            // 
            // _depositCancelButton
            // 
            this._depositCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._depositCancelButton.Location = new System.Drawing.Point(142, 205);
            this._depositCancelButton.Name = "_depositCancelButton";
            this._depositCancelButton.Size = new System.Drawing.Size(75, 23);
            this._depositCancelButton.TabIndex = 2;
            this._depositCancelButton.Text = "Cancel";
            this._depositCancelButton.UseVisualStyleBackColor = true;
            // 
            // _depositButton
            // 
            this._depositButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._depositButton.Location = new System.Drawing.Point(223, 205);
            this._depositButton.Name = "_depositButton";
            this._depositButton.Size = new System.Drawing.Size(75, 23);
            this._depositButton.TabIndex = 1;
            this._depositButton.Text = "Deposit";
            this._depositButton.UseVisualStyleBackColor = true;
            // 
            // _withdrawalTab
            // 
            this._withdrawalTab.Controls.Add(this.groupBox6);
            this._withdrawalTab.Controls.Add(this._withdrawalCancelButton);
            this._withdrawalTab.Controls.Add(this._withdrawalButton);
            this._withdrawalTab.Location = new System.Drawing.Point(4, 22);
            this._withdrawalTab.Name = "_withdrawalTab";
            this._withdrawalTab.Size = new System.Drawing.Size(298, 228);
            this._withdrawalTab.TabIndex = 2;
            this._withdrawalTab.Text = "tabPage3";
            this._withdrawalTab.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this._withdrawalAmount);
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(285, 49);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Specify the amount to withdrawal";
            // 
            // _withdrawalAmount
            // 
            this._withdrawalAmount.Location = new System.Drawing.Point(6, 19);
            this._withdrawalAmount.Name = "_withdrawalAmount";
            this._withdrawalAmount.Size = new System.Drawing.Size(273, 20);
            this._withdrawalAmount.TabIndex = 0;
            this._withdrawalAmount.Text = "0";
            this._withdrawalAmount.TextChanged += new System.EventHandler(this._depositAmount_TextChanged);
            this._withdrawalAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._amount_KeyPress);
            // 
            // _withdrawalCancelButton
            // 
            this._withdrawalCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._withdrawalCancelButton.Location = new System.Drawing.Point(142, 205);
            this._withdrawalCancelButton.Name = "_withdrawalCancelButton";
            this._withdrawalCancelButton.Size = new System.Drawing.Size(75, 23);
            this._withdrawalCancelButton.TabIndex = 2;
            this._withdrawalCancelButton.Text = "Cancel";
            this._withdrawalCancelButton.UseVisualStyleBackColor = true;
            // 
            // _withdrawalButton
            // 
            this._withdrawalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._withdrawalButton.Location = new System.Drawing.Point(223, 205);
            this._withdrawalButton.Name = "_withdrawalButton";
            this._withdrawalButton.Size = new System.Drawing.Size(75, 23);
            this._withdrawalButton.TabIndex = 1;
            this._withdrawalButton.Text = "Withdrawal";
            this._withdrawalButton.UseVisualStyleBackColor = true;
            // 
            // _transferTab
            // 
            this._transferTab.Controls.Add(this.groupBox2);
            this._transferTab.Controls.Add(this._transferCancelButton);
            this._transferTab.Controls.Add(this._transferButton);
            this._transferTab.Location = new System.Drawing.Point(4, 22);
            this._transferTab.Name = "_transferTab";
            this._transferTab.Size = new System.Drawing.Size(298, 228);
            this._transferTab.TabIndex = 3;
            this._transferTab.Text = "tabPage4";
            this._transferTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._transferAccounts);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this._transferAmount);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 102);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Specify the clients new address";
            // 
            // _transferAccounts
            // 
            this._transferAccounts.FormattingEnabled = true;
            this._transferAccounts.Location = new System.Drawing.Point(6, 72);
            this._transferAccounts.Name = "_transferAccounts";
            this._transferAccounts.Size = new System.Drawing.Size(273, 21);
            this._transferAccounts.TabIndex = 1;
            this._transferAccounts.SelectedIndexChanged += new System.EventHandler(this._depositAmount_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Account to transfer to";
            // 
            // _transferAmount
            // 
            this._transferAmount.Location = new System.Drawing.Point(6, 33);
            this._transferAmount.Name = "_transferAmount";
            this._transferAmount.Size = new System.Drawing.Size(273, 20);
            this._transferAmount.TabIndex = 0;
            this._transferAmount.Text = "0";
            this._transferAmount.TextChanged += new System.EventHandler(this._depositAmount_TextChanged);
            this._transferAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._amount_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Amount to be transferred";
            // 
            // _transferCancelButton
            // 
            this._transferCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._transferCancelButton.Location = new System.Drawing.Point(142, 205);
            this._transferCancelButton.Name = "_transferCancelButton";
            this._transferCancelButton.Size = new System.Drawing.Size(75, 23);
            this._transferCancelButton.TabIndex = 3;
            this._transferCancelButton.Text = "Cancel";
            this._transferCancelButton.UseVisualStyleBackColor = true;
            // 
            // _transferButton
            // 
            this._transferButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._transferButton.Location = new System.Drawing.Point(223, 205);
            this._transferButton.Name = "_transferButton";
            this._transferButton.Size = new System.Drawing.Size(75, 23);
            this._transferButton.TabIndex = 2;
            this._transferButton.Text = "Transfer";
            this._transferButton.UseVisualStyleBackColor = true;
            // 
            // _nameChangeTab
            // 
            this._nameChangeTab.Controls.Add(this.groupBox7);
            this._nameChangeTab.Controls.Add(this._newAccountNameCancelButton);
            this._nameChangeTab.Controls.Add(this._newAccountNameSaveButton);
            this._nameChangeTab.Location = new System.Drawing.Point(4, 22);
            this._nameChangeTab.Name = "_nameChangeTab";
            this._nameChangeTab.Size = new System.Drawing.Size(298, 228);
            this._nameChangeTab.TabIndex = 4;
            this._nameChangeTab.Text = "tabPage5";
            this._nameChangeTab.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this._accountName);
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(285, 49);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Specify the new account name";
            // 
            // _accountName
            // 
            this._accountName.Location = new System.Drawing.Point(6, 19);
            this._accountName.Name = "_accountName";
            this._accountName.Size = new System.Drawing.Size(273, 20);
            this._accountName.TabIndex = 0;
            this._accountName.TextChanged += new System.EventHandler(this._depositAmount_TextChanged);
            // 
            // _newAccountNameCancelButton
            // 
            this._newAccountNameCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._newAccountNameCancelButton.Location = new System.Drawing.Point(142, 205);
            this._newAccountNameCancelButton.Name = "_newAccountNameCancelButton";
            this._newAccountNameCancelButton.Size = new System.Drawing.Size(75, 23);
            this._newAccountNameCancelButton.TabIndex = 2;
            this._newAccountNameCancelButton.Text = "Cancel";
            this._newAccountNameCancelButton.UseVisualStyleBackColor = true;
            // 
            // _newAccountNameSaveButton
            // 
            this._newAccountNameSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._newAccountNameSaveButton.Location = new System.Drawing.Point(223, 205);
            this._newAccountNameSaveButton.Name = "_newAccountNameSaveButton";
            this._newAccountNameSaveButton.Size = new System.Drawing.Size(75, 23);
            this._newAccountNameSaveButton.TabIndex = 1;
            this._newAccountNameSaveButton.Text = "Save";
            this._newAccountNameSaveButton.UseVisualStyleBackColor = true;
            // 
            // AccountDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 278);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountDetails";
            this.Text = "Account Details";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this._detailsTab.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this._depositTab.ResumeLayout(false);
            this._phoneNumberGroupBox.ResumeLayout(false);
            this._phoneNumberGroupBox.PerformLayout();
            this._withdrawalTab.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this._transferTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this._nameChangeTab.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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