namespace CryptokiExplorer
{
    partial class ChangePINForm
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
            this.textBoxNewPIN1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNewPIN2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textBoxOldPIN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxNewPIN1
            // 
            this.textBoxNewPIN1.Location = new System.Drawing.Point(112, 47);
            this.textBoxNewPIN1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNewPIN1.Name = "textBoxNewPIN1";
            this.textBoxNewPIN1.Size = new System.Drawing.Size(157, 22);
            this.textBoxNewPIN1.TabIndex = 3;
            this.textBoxNewPIN1.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "New PIN";
            // 
            // textBoxNewPIN2
            // 
            this.textBoxNewPIN2.Location = new System.Drawing.Point(112, 79);
            this.textBoxNewPIN2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNewPIN2.Name = "textBoxNewPIN2";
            this.textBoxNewPIN2.Size = new System.Drawing.Size(157, 22);
            this.textBoxNewPIN2.TabIndex = 5;
            this.textBoxNewPIN2.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Confirm PIN";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(29, 118);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(156, 117);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Annulla";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxOldPIN
            // 
            this.textBoxOldPIN.Location = new System.Drawing.Point(112, 15);
            this.textBoxOldPIN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxOldPIN.Name = "textBoxOldPIN";
            this.textBoxOldPIN.Size = new System.Drawing.Size(156, 22);
            this.textBoxOldPIN.TabIndex = 1;
            this.textBoxOldPIN.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "PIN";
            // 
            // ChangePINForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(290, 160);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBoxNewPIN2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNewPIN1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOldPIN);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ChangePINForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change PIN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNewPIN2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox textBoxNewPIN1;
        public System.Windows.Forms.TextBox textBoxOldPIN;
        private System.Windows.Forms.Label label1;
    }
}