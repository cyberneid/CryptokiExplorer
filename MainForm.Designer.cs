namespace CryptokiExplorer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unblockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.listViewContent = new System.Windows.Forms.ListView();
            this.contextMenuObject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExplore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbPIN = new System.Windows.Forms.Label();
            this.textBoxPIN = new System.Windows.Forms.TextBox();
            this.btnVerifyPIN = new System.Windows.Forms.Button();
            this.comboBoxReader = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePINToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unblockPINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pictureBoxPIN = new System.Windows.Forms.PictureBox();
            this.pictureBoxReader = new System.Windows.Forms.PictureBox();
            this.btnData = new System.Windows.Forms.Button();
            this.btnMechanism = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnCertificate = new System.Windows.Forms.Button();
            this.btnKey = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.contextMenuObject.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPIN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.pINToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.cancel1;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem1});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Image = global::CryptokiExplorer.Properties.Resources.cog;
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            resources.ApplyResources(this.settingsToolStripMenuItem1, "settingsToolStripMenuItem1");
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // pINToolStripMenuItem
            // 
            this.pINToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePINToolStripMenuItem,
            this.unblockToolStripMenuItem});
            this.pINToolStripMenuItem.Name = "pINToolStripMenuItem";
            resources.ApplyResources(this.pINToolStripMenuItem, "pINToolStripMenuItem");
            this.pINToolStripMenuItem.DropDownOpening += new System.EventHandler(this.pINToolStripMenuItem_DropDownOpening);
            // 
            // changePINToolStripMenuItem
            // 
            this.changePINToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.wrench_orange;
            this.changePINToolStripMenuItem.Name = "changePINToolStripMenuItem";
            resources.ApplyResources(this.changePINToolStripMenuItem, "changePINToolStripMenuItem");
            this.changePINToolStripMenuItem.Click += new System.EventHandler(this.changePINToolStripMenuItem_Click);
            // 
            // unblockToolStripMenuItem
            // 
            this.unblockToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.lock_open;
            this.unblockToolStripMenuItem.Name = "unblockToolStripMenuItem";
            resources.ApplyResources(this.unblockToolStripMenuItem, "unblockToolStripMenuItem");
            this.unblockToolStripMenuItem.Click += new System.EventHandler(this.unblockToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.help;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "SmartCardReader2.gif");
            this.imageList.Images.SetKeyName(1, "information.png");
            this.imageList.Images.SetKeyName(2, "medal_bronze_3.png");
            this.imageList.Images.SetKeyName(3, "key.png");
            this.imageList.Images.SetKeyName(4, "package.png");
            this.imageList.Images.SetKeyName(5, "cog.png");
            this.imageList.Images.SetKeyName(6, "smartcard.png");
            this.imageList.Images.SetKeyName(7, "keyp.png");
            // 
            // listViewContent
            // 
            resources.ApplyResources(this.listViewContent, "listViewContent");
            this.listViewContent.ContextMenuStrip = this.contextMenuObject;
            this.listViewContent.FullRowSelect = true;
            this.listViewContent.HideSelection = false;
            this.listViewContent.MultiSelect = false;
            this.listViewContent.Name = "listViewContent";
            this.listViewContent.ShowItemToolTips = true;
            this.listViewContent.SmallImageList = this.imageList;
            this.listViewContent.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewContent.UseCompatibleStateImageBehavior = false;
            this.listViewContent.View = System.Windows.Forms.View.Details;
            this.listViewContent.DoubleClick += new System.EventHandler(this.listViewContent_DoubleClick);
            this.listViewContent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewContent_MouseClick);
            // 
            // contextMenuObject
            // 
            this.contextMenuObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExplore,
            this.toolStripMenuItemExport,
            this.importToolStripMenuItem,
            this.toolStripMenuItemView,
            this.deleteToolStripMenuItem});
            this.contextMenuObject.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuObject, "contextMenuObject");
            this.contextMenuObject.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuObject_Opening);
            // 
            // toolStripMenuItemExplore
            // 
            this.toolStripMenuItemExplore.Image = global::CryptokiExplorer.Properties.Resources.magnifier;
            this.toolStripMenuItemExplore.Name = "toolStripMenuItemExplore";
            resources.ApplyResources(this.toolStripMenuItemExplore, "toolStripMenuItemExplore");
            this.toolStripMenuItemExplore.Click += new System.EventHandler(this.toolStripMenuItemExplore_Click);
            // 
            // toolStripMenuItemExport
            // 
            this.toolStripMenuItemExport.Image = global::CryptokiExplorer.Properties.Resources.application_side_contract;
            this.toolStripMenuItemExport.Name = "toolStripMenuItemExport";
            resources.ApplyResources(this.toolStripMenuItemExport, "toolStripMenuItemExport");
            this.toolStripMenuItemExport.Click += new System.EventHandler(this.toolStripMenuItemExport_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.application_side_expand;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            resources.ApplyResources(this.importToolStripMenuItem, "importToolStripMenuItem");
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // toolStripMenuItemView
            // 
            this.toolStripMenuItemView.Image = global::CryptokiExplorer.Properties.Resources.eye;
            this.toolStripMenuItemView.Name = "toolStripMenuItemView";
            resources.ApplyResources(this.toolStripMenuItemView, "toolStripMenuItemView");
            this.toolStripMenuItemView.Click += new System.EventHandler(this.toolStripMenuItemView_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.cancel1;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // lbPIN
            // 
            resources.ApplyResources(this.lbPIN, "lbPIN");
            this.lbPIN.Name = "lbPIN";
            // 
            // textBoxPIN
            // 
            resources.ApplyResources(this.textBoxPIN, "textBoxPIN");
            this.textBoxPIN.Name = "textBoxPIN";
            this.textBoxPIN.UseSystemPasswordChar = true;
            // 
            // btnVerifyPIN
            // 
            resources.ApplyResources(this.btnVerifyPIN, "btnVerifyPIN");
            this.btnVerifyPIN.Name = "btnVerifyPIN";
            this.btnVerifyPIN.UseVisualStyleBackColor = true;
            this.btnVerifyPIN.Click += new System.EventHandler(this.btnVerifyPIN_Click);
            // 
            // comboBoxReader
            // 
            this.comboBoxReader.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxReader, "comboBoxReader");
            this.comboBoxReader.Name = "comboBoxReader";
            this.comboBoxReader.SelectedIndexChanged += new System.EventHandler(this.comboBoxReader_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Image = global::CryptokiExplorer.Properties.Resources.cancel1;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnImport, "btnImport");
            this.btnImport.Image = global::CryptokiExplorer.Properties.Resources.application_side_expand;
            this.btnImport.Name = "btnImport";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolsToolStripMenuItem,
            this.changePINToolStripMenuItem1,
            this.unblockPINToolStripMenuItem,
            this.aboutToolStripMenuItem1,
            this.exitToolStripMenuItem1});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // showToolsToolStripMenuItem
            // 
            this.showToolsToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.application_view_gallery;
            resources.ApplyResources(this.showToolsToolStripMenuItem, "showToolsToolStripMenuItem");
            this.showToolsToolStripMenuItem.Name = "showToolsToolStripMenuItem";
            this.showToolsToolStripMenuItem.Click += new System.EventHandler(this.showToolsToolStripMenuItem_Click);
            // 
            // changePINToolStripMenuItem1
            // 
            this.changePINToolStripMenuItem1.Image = global::CryptokiExplorer.Properties.Resources.wrench_orange;
            this.changePINToolStripMenuItem1.Name = "changePINToolStripMenuItem1";
            resources.ApplyResources(this.changePINToolStripMenuItem1, "changePINToolStripMenuItem1");
            this.changePINToolStripMenuItem1.Click += new System.EventHandler(this.changePINToolStripMenuItem1_Click);
            // 
            // unblockPINToolStripMenuItem
            // 
            this.unblockPINToolStripMenuItem.Image = global::CryptokiExplorer.Properties.Resources.lock_open;
            this.unblockPINToolStripMenuItem.Name = "unblockPINToolStripMenuItem";
            resources.ApplyResources(this.unblockPINToolStripMenuItem, "unblockPINToolStripMenuItem");
            this.unblockPINToolStripMenuItem.Click += new System.EventHandler(this.unblockPINToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Image = global::CryptokiExplorer.Properties.Resources.help;
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            resources.ApplyResources(this.aboutToolStripMenuItem1, "aboutToolStripMenuItem1");
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Image = global::CryptokiExplorer.Properties.Resources.cancel1;
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            resources.ApplyResources(this.exitToolStripMenuItem1, "exitToolStripMenuItem1");
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Image = global::CryptokiExplorer.Properties.Resources.arrow_refresh;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pictureBoxPIN
            // 
            this.pictureBoxPIN.Image = global::CryptokiExplorer.Properties.Resources.delete;
            resources.ApplyResources(this.pictureBoxPIN, "pictureBoxPIN");
            this.pictureBoxPIN.Name = "pictureBoxPIN";
            this.pictureBoxPIN.TabStop = false;
            // 
            // pictureBoxReader
            // 
            this.pictureBoxReader.Image = global::CryptokiExplorer.Properties.Resources.delete;
            resources.ApplyResources(this.pictureBoxReader, "pictureBoxReader");
            this.pictureBoxReader.Name = "pictureBoxReader";
            this.pictureBoxReader.TabStop = false;
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnData, "btnData");
            this.btnData.ImageList = this.imageList;
            this.btnData.Name = "btnData";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // btnMechanism
            // 
            this.btnMechanism.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnMechanism, "btnMechanism");
            this.btnMechanism.ImageList = this.imageList;
            this.btnMechanism.Name = "btnMechanism";
            this.btnMechanism.UseVisualStyleBackColor = false;
            this.btnMechanism.Click += new System.EventHandler(this.btnMechanism_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Image = global::CryptokiExplorer.Properties.Resources.application_side_contract;
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnInfo, "btnInfo");
            this.btnInfo.ImageList = this.imageList;
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnCertificate
            // 
            this.btnCertificate.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnCertificate, "btnCertificate");
            this.btnCertificate.ImageList = this.imageList;
            this.btnCertificate.Name = "btnCertificate";
            this.btnCertificate.UseVisualStyleBackColor = false;
            this.btnCertificate.Click += new System.EventHandler(this.btnCertificate_Click);
            // 
            // btnKey
            // 
            this.btnKey.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.btnKey, "btnKey");
            this.btnKey.ImageList = this.imageList;
            this.btnKey.Name = "btnKey";
            this.btnKey.UseVisualStyleBackColor = false;
            this.btnKey.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.pictureBoxPIN);
            this.Controls.Add(this.pictureBoxReader);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnMechanism);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnCertificate);
            this.Controls.Add(this.btnKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxReader);
            this.Controls.Add(this.lbPIN);
            this.Controls.Add(this.textBoxPIN);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnVerifyPIN);
            this.Controls.Add(this.listViewContent);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuObject.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPIN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pINToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePINToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unblockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListView listViewContent;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label lbPIN;
        private System.Windows.Forms.TextBox textBoxPIN;
        private System.Windows.Forms.Button btnVerifyPIN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxReader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKey;
        private System.Windows.Forms.Button btnCertificate;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMechanism;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxPIN;
        private System.Windows.Forms.PictureBox pictureBoxReader;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changePINToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem unblockPINToolStripMenuItem;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuObject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExplore;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}

