namespace Fohjin.DDD.BankApplication.Views
{
    partial class ClientSearchForm
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
            this._clients = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateNewClientButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _clients
            // 
            this._clients.FormattingEnabled = true;
            this._clients.Location = new System.Drawing.Point(12, 25);
            this._clients.Name = "_clients";
            this._clients.Size = new System.Drawing.Size(360, 95);
            this._clients.TabIndex = 0;
            this._clients.DoubleClick += new System.EventHandler(this._clients_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Clients";
            // 
            // CreateNewClientButton
            // 
            this.CreateNewClientButton.Location = new System.Drawing.Point(242, 126);
            this.CreateNewClientButton.Name = "CreateNewClientButton";
            this.CreateNewClientButton.Size = new System.Drawing.Size(130, 23);
            this.CreateNewClientButton.TabIndex = 2;
            this.CreateNewClientButton.Text = "Create new Client";
            this.CreateNewClientButton.UseVisualStyleBackColor = true;
            this.CreateNewClientButton.Click += new System.EventHandler(this.CreateNewClientButton_Click);
            // 
            // ClientSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 159);
            this.Controls.Add(this.CreateNewClientButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._clients);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientSearchForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Client Search Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _clients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CreateNewClientButton;
    }
}