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
            this.label2 = new System.Windows.Forms.Label();
            this.CloseAccountButton = new System.Windows.Forms.Button();
            this.SaveAccountButton = new System.Windows.Forms.Button();
            this._ledgers = new System.Windows.Forms.ListBox();
            this._accountName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._amount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.DepositeButton);
            this.groupBox1.Controls.Add(this.WithdrawlButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 56);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transer Money";
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
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CloseAccountButton);
            this.groupBox2.Controls.Add(this.SaveAccountButton);
            this.groupBox2.Controls.Add(this._ledgers);
            this.groupBox2.Controls.Add(this._accountName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 203);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Account Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Ledgers";
            // 
            // CloseAccountButton
            // 
            this.CloseAccountButton.Location = new System.Drawing.Point(146, 170);
            this.CloseAccountButton.Name = "CloseAccountButton";
            this.CloseAccountButton.Size = new System.Drawing.Size(110, 23);
            this.CloseAccountButton.TabIndex = 21;
            this.CloseAccountButton.Text = "Close Account";
            this.CloseAccountButton.UseVisualStyleBackColor = true;
            this.CloseAccountButton.Click += new System.EventHandler(this.CloseAccountButton_Click);
            // 
            // SaveAccountButton
            // 
            this.SaveAccountButton.Location = new System.Drawing.Point(262, 170);
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
            this._ledgers.Location = new System.Drawing.Point(12, 69);
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
            // AccountDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 270);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AccountDetails";
            this.Text = "Account Details";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}