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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addANewClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshExistingClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _clients
            // 
            this._clients.FormattingEnabled = true;
            this._clients.Location = new System.Drawing.Point(12, 52);
            this._clients.Name = "_clients";
            this._clients.Size = new System.Drawing.Size(360, 95);
            this._clients.TabIndex = 0;
            this._clients.DoubleClick += new System.EventHandler(this._clients_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Existing Clients";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clientToolStripMenuItem
            // 
            this.clientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addANewClientToolStripMenuItem,
            this.refreshExistingClientsToolStripMenuItem});
            this.clientToolStripMenuItem.Name = "clientToolStripMenuItem";
            this.clientToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.clientToolStripMenuItem.Text = "Client";
            // 
            // addANewClientToolStripMenuItem
            // 
            this.addANewClientToolStripMenuItem.Name = "addANewClientToolStripMenuItem";
            this.addANewClientToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.addANewClientToolStripMenuItem.Text = "Add a new client";
            this.addANewClientToolStripMenuItem.Click += new System.EventHandler(this.addANewClientToolStripMenuItem_Click);
            // 
            // refreshExistingClientsToolStripMenuItem
            // 
            this.refreshExistingClientsToolStripMenuItem.Name = "refreshExistingClientsToolStripMenuItem";
            this.refreshExistingClientsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.refreshExistingClientsToolStripMenuItem.Text = "Refresh existing clients";
            this.refreshExistingClientsToolStripMenuItem.Click += new System.EventHandler(this.refreshExistingClientsToolStripMenuItem_Click);
            // 
            // ClientSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 159);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._clients);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientSearchForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Client Search Form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _clients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addANewClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshExistingClientsToolStripMenuItem;
    }
}