namespace Fohjin.DDD.BankApplication.Views
{
    partial class Popup
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
            this._exception = new System.Windows.Forms.GroupBox();
            this._message = new System.Windows.Forms.TextBox();
            this._exception.SuspendLayout();
            this.SuspendLayout();
            // 
            // _exception
            // 
            this._exception.Controls.Add(this._message);
            this._exception.Dock = System.Windows.Forms.DockStyle.Fill;
            this._exception.Location = new System.Drawing.Point(0, 0);
            this._exception.Name = "_exception";
            this._exception.Size = new System.Drawing.Size(554, 62);
            this._exception.TabIndex = 0;
            this._exception.TabStop = false;
            this._exception.Text = "Exception";
            // 
            // _message
            // 
            this._message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._message.Location = new System.Drawing.Point(7, 20);
            this._message.Multiline = true;
            this._message.Name = "_message";
            this._message.ReadOnly = true;
            this._message.Size = new System.Drawing.Size(541, 36);
            this._message.TabIndex = 0;
            this._message.TabStop = false;
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 62);
            this.Controls.Add(this._exception);
            this.Name = "Popup";
            this.Text = "An exception occurred";
            this._exception.ResumeLayout(false);
            this._exception.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _exception;
        private System.Windows.Forms.TextBox _message;
    }
}