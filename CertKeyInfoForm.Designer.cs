namespace CryptokiExplorer
{
    partial class CertKeyInfoForm
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
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxPrivate = new System.Windows.Forms.CheckBox();
            this.checkBoxModifiable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Location = new System.Drawing.Point(63, 47);
            this.textBoxLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(259, 22);
            this.textBoxLabel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Label";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(57, 122);
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
            this.btnCancel.Location = new System.Drawing.Point(184, 121);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Annulla";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(63, 15);
            this.textBoxID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(259, 22);
            this.textBoxID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // checkBoxPrivate
            // 
            this.checkBoxPrivate.AutoSize = true;
            this.checkBoxPrivate.Location = new System.Drawing.Point(20, 89);
            this.checkBoxPrivate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxPrivate.Name = "checkBoxPrivate";
            this.checkBoxPrivate.Size = new System.Drawing.Size(74, 21);
            this.checkBoxPrivate.TabIndex = 8;
            this.checkBoxPrivate.Text = "Private";
            this.checkBoxPrivate.UseVisualStyleBackColor = true;
            // 
            // checkBoxModifiable
            // 
            this.checkBoxModifiable.AutoSize = true;
            this.checkBoxModifiable.Location = new System.Drawing.Point(107, 89);
            this.checkBoxModifiable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxModifiable.Name = "checkBoxModifiable";
            this.checkBoxModifiable.Size = new System.Drawing.Size(94, 21);
            this.checkBoxModifiable.TabIndex = 9;
            this.checkBoxModifiable.Text = "Modifiable";
            this.checkBoxModifiable.UseVisualStyleBackColor = true;
            // 
            // CertKeyInfoForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(343, 178);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxModifiable);
            this.Controls.Add(this.checkBoxPrivate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBoxLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CertKeyInfoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Certificate / Key Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox textBoxLabel;
        public System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox checkBoxPrivate;
        public System.Windows.Forms.CheckBox checkBoxModifiable;
    }
}