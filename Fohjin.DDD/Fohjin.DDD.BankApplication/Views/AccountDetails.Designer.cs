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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._amount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DepositeButton = new System.Windows.Forms.Button();
            this.WithdrawlButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._accountNumber = new System.Windows.Forms.Label();
            this._balance = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CloseAccountButton = new System.Windows.Forms.Button();
            this.SaveAccountButton = new System.Windows.Forms.Button();
            this._ledgers = new System.Windows.Forms.ListBox();
            this._accountName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._transferAccounts = new System.Windows.Forms.ComboBox();
            this._transferAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TransferButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._amount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.DepositeButton);
            this.groupBox1.Controls.Add(this.WithdrawlButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 273);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 56);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manually Transer Money";
            // 
            // _amount
            // 
            this._amount.Location = new System.Drawing.Point(73, 21);
            this._amount.Name = "_amount";
            this._amount.Size = new System.Drawing.Size(64, 20);
            this._amount.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Amount";
            // 
            // DepositeButton
            // 
            this.DepositeButton.Location = new System.Drawing.Point(146, 20);
            this.DepositeButton.Name = "DepositeButton";
            this.DepositeButton.Size = new System.Drawing.Size(110, 23);
            this.DepositeButton.TabIndex = 20;
            this.DepositeButton.Text = "Deposite";
            this.DepositeButton.UseVisualStyleBackColor = true;
            this.DepositeButton.Click += new System.EventHandler(this.DepositeButton_Click);
            // 
            // WithdrawlButton
            // 
            this.WithdrawlButton.Location = new System.Drawing.Point(262, 20);
            this.WithdrawlButton.Name = "WithdrawlButton";
            this.WithdrawlButton.Size = new System.Drawing.Size(110, 23);
            this.WithdrawlButton.TabIndex = 19;
            this.WithdrawlButton.Text = "Withdrawl";
            this.WithdrawlButton.UseVisualStyleBackColor = true;
            this.WithdrawlButton.Click += new System.EventHandler(this.WithdrawlButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._accountNumber);
            this.groupBox2.Controls.Add(this._balance);
            this.groupBox2.Controls.Add(this.label);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CloseAccountButton);
            this.groupBox2.Controls.Add(this.SaveAccountButton);
            this.groupBox2.Controls.Add(this._ledgers);
            this.groupBox2.Controls.Add(this._accountName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 265);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Account Details";
            // 
            // _accountNumber
            // 
            this._accountNumber.AutoSize = true;
            this._accountNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._accountNumber.Location = new System.Drawing.Point(109, 78);
            this._accountNumber.Name = "_accountNumber";
            this._accountNumber.Size = new System.Drawing.Size(14, 13);
            this._accountNumber.TabIndex = 28;
            this._accountNumber.Text = "0";
            // 
            // _balance
            // 
            this._balance.AutoSize = true;
            this._balance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._balance.Location = new System.Drawing.Point(109, 52);
            this._balance.Name = "_balance";
            this._balance.Size = new System.Drawing.Size(14, 13);
            this._balance.TabIndex = 27;
            this._balance.Text = "0";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(9, 78);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(85, 13);
            this.label.TabIndex = 25;
            this.label.Text = "Account number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Account balance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Ledgers";
            // 
            // CloseAccountButton
            // 
            this.CloseAccountButton.Location = new System.Drawing.Point(146, 232);
            this.CloseAccountButton.Name = "CloseAccountButton";
            this.CloseAccountButton.Size = new System.Drawing.Size(110, 23);
            this.CloseAccountButton.TabIndex = 21;
            this.CloseAccountButton.Text = "Close Account";
            this.CloseAccountButton.UseVisualStyleBackColor = true;
            this.CloseAccountButton.Click += new System.EventHandler(this.CloseAccountButton_Click);
            // 
            // SaveAccountButton
            // 
            this.SaveAccountButton.Location = new System.Drawing.Point(262, 232);
            this.SaveAccountButton.Name = "SaveAccountButton";
            this.SaveAccountButton.Size = new System.Drawing.Size(110, 23);
            this.SaveAccountButton.TabIndex = 20;
            this.SaveAccountButton.Text = "Save Account";
            this.SaveAccountButton.UseVisualStyleBackColor = true;
            this.SaveAccountButton.Click += new System.EventHandler(this.SaveAccountButton_Click);
            // 
            // _ledgers
            // 
            this._ledgers.FormattingEnabled = true;
            this._ledgers.Location = new System.Drawing.Point(12, 131);
            this._ledgers.Name = "_ledgers";
            this._ledgers.Size = new System.Drawing.Size(360, 95);
            this._ledgers.TabIndex = 19;
            // 
            // _accountName
            // 
            this._accountName.Location = new System.Drawing.Point(112, 23);
            this._accountName.Name = "_accountName";
            this._accountName.Size = new System.Drawing.Size(260, 20);
            this._accountName.TabIndex = 18;
            this._accountName.TextChanged += new System.EventHandler(this._client_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._transferAccounts);
            this.groupBox3.Controls.Add(this._transferAmount);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.TransferButton);
            this.groupBox3.Location = new System.Drawing.Point(3, 335);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 88);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transer Money";
            // 
            // _transferAccounts
            // 
            this._transferAccounts.FormattingEnabled = true;
            this._transferAccounts.Location = new System.Drawing.Point(146, 22);
            this._transferAccounts.Name = "_transferAccounts";
            this._transferAccounts.Size = new System.Drawing.Size(224, 21);
            this._transferAccounts.TabIndex = 23;
            // 
            // _transferAmount
            // 
            this._transferAmount.Location = new System.Drawing.Point(73, 21);
            this._transferAmount.Name = "_transferAmount";
            this._transferAmount.Size = new System.Drawing.Size(64, 20);
            this._transferAmount.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Amount";
            // 
            // TransferButton
            // 
            this.TransferButton.Location = new System.Drawing.Point(262, 53);
            this.TransferButton.Name = "TransferButton";
            this.TransferButton.Size = new System.Drawing.Size(110, 23);
            this.TransferButton.TabIndex = 19;
            this.TransferButton.Text = "Transfer";
            this.TransferButton.UseVisualStyleBackColor = true;
            // 
            // AccountDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 430);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountDetails";
            this.Text = "Account Details";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _amount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DepositeButton;
        private System.Windows.Forms.Button WithdrawlButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CloseAccountButton;
        private System.Windows.Forms.Button SaveAccountButton;
        private System.Windows.Forms.ListBox _ledgers;
        private System.Windows.Forms.TextBox _accountName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox _transferAccounts;
        private System.Windows.Forms.TextBox _transferAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button TransferButton;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label _balance;
        private System.Windows.Forms.Label _accountNumber;
    }
}