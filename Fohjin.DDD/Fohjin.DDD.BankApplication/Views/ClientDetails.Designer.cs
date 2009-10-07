namespace Fohjin.DDD.BankApplication.Views
{
    partial class ClientDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this._clientName = new System.Windows.Forms.TextBox();
            this._street = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._city = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._streetNumber = new System.Windows.Forms.TextBox();
            this._postalCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._accounts = new System.Windows.Forms.ListBox();
            this.SaveClientButton = new System.Windows.Forms.Button();
            this.AddNewAccountButton = new System.Windows.Forms.Button();
            this._phoneNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // _clientName
            // 
            this._clientName.Location = new System.Drawing.Point(112, 10);
            this._clientName.Name = "_clientName";
            this._clientName.Size = new System.Drawing.Size(260, 20);
            this._clientName.TabIndex = 1;
            // 
            // _street
            // 
            this._street.Location = new System.Drawing.Point(112, 45);
            this._street.Name = "_street";
            this._street.Size = new System.Drawing.Size(205, 20);
            this._street.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address / Number";
            // 
            // _city
            // 
            this._city.Location = new System.Drawing.Point(167, 71);
            this._city.Name = "_city";
            this._city.Size = new System.Drawing.Size(205, 20);
            this._city.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Postalcode / City";
            // 
            // _streetNumber
            // 
            this._streetNumber.Location = new System.Drawing.Point(323, 45);
            this._streetNumber.Name = "_streetNumber";
            this._streetNumber.Size = new System.Drawing.Size(49, 20);
            this._streetNumber.TabIndex = 6;
            // 
            // _postalCode
            // 
            this._postalCode.Location = new System.Drawing.Point(112, 71);
            this._postalCode.Name = "_postalCode";
            this._postalCode.Size = new System.Drawing.Size(49, 20);
            this._postalCode.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Accounts";
            // 
            // _accounts
            // 
            this._accounts.FormattingEnabled = true;
            this._accounts.Location = new System.Drawing.Point(12, 149);
            this._accounts.Name = "_accounts";
            this._accounts.Size = new System.Drawing.Size(360, 95);
            this._accounts.TabIndex = 9;
            this._accounts.SelectedIndexChanged += new System.EventHandler(this._accounts_SelectedIndexChanged);
            // 
            // SaveClientButton
            // 
            this.SaveClientButton.Location = new System.Drawing.Point(242, 250);
            this.SaveClientButton.Name = "SaveClientButton";
            this.SaveClientButton.Size = new System.Drawing.Size(130, 23);
            this.SaveClientButton.TabIndex = 10;
            this.SaveClientButton.Text = "Save Client";
            this.SaveClientButton.UseVisualStyleBackColor = true;
            this.SaveClientButton.Click += new System.EventHandler(this.SaveClientButton_Click);
            // 
            // AddNewAccountButton
            // 
            this.AddNewAccountButton.Location = new System.Drawing.Point(106, 250);
            this.AddNewAccountButton.Name = "AddNewAccountButton";
            this.AddNewAccountButton.Size = new System.Drawing.Size(130, 23);
            this.AddNewAccountButton.TabIndex = 10;
            this.AddNewAccountButton.Text = "Add new Account";
            this.AddNewAccountButton.UseVisualStyleBackColor = true;
            this.AddNewAccountButton.Click += new System.EventHandler(this.AddNewAccountButton_Click);
            // 
            // _phoneNumber
            // 
            this._phoneNumber.Location = new System.Drawing.Point(112, 97);
            this._phoneNumber.Name = "_phoneNumber";
            this._phoneNumber.Size = new System.Drawing.Size(260, 20);
            this._phoneNumber.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Phone number";
            // 
            // ClientDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 287);
            this.Controls.Add(this._phoneNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AddNewAccountButton);
            this.Controls.Add(this.SaveClientButton);
            this.Controls.Add(this._accounts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._postalCode);
            this.Controls.Add(this._streetNumber);
            this.Controls.Add(this._city);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._street);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._clientName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientDetails";
            this.Text = "Client Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _clientName;
        private System.Windows.Forms.TextBox _street;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _city;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _streetNumber;
        private System.Windows.Forms.TextBox _postalCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox _accounts;
        private System.Windows.Forms.Button SaveClientButton;
        private System.Windows.Forms.Button AddNewAccountButton;
        private System.Windows.Forms.TextBox _phoneNumber;
        private System.Windows.Forms.Label label5;
    }
}