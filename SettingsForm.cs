using System;
using System.Windows.Forms;
using Cyberneid.NCryptoki;

namespace CryptokiExplorer
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            try
            {
                textBoxPKCS11.Text = Properties.Settings.Default.CryptokiPath.Replace("/", "\\");
            }
            catch (Exception)
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(textBoxPKCS11.Text.Equals(""))      
                MessageBox.Show("Error", "Select a valid PKCS#11 module", MessageBoxButtons.OK, MessageBoxIcon.Error);

            try
            {
                Cryptoki c = new Cryptoki(textBoxPKCS11.Text);
                Properties.Settings.Default.CryptokiPath = textBoxPKCS11.Text.Replace("\\", "/");
                Properties.Settings.Default.Save();
            }
            catch (CryptokiException ex)
            {
                if (ex.ErrorCode == CryptokiException.CKR_TRIAL_EXPIRED)
                {
                    MessageBox.Show("The 30 days trial period is expired.\nVisit http://www.ncryptoki.com for more info about the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("The selected PKCS#11 module seems to be invalid. Error code: " + ex.ErrorCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                DialogResult = DialogResult.Cancel;
            }            
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBoxPKCS11.Text = Properties.Settings.Default.CryptokiPath;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.AddExtension = true;

            dlgOpen.DefaultExt = "dll";
            dlgOpen.Filter = "PKCS#11 dll (*.dll)|*.dll|All Files (*.*)|*.*";
            if (dlgOpen.ShowDialog(this) == DialogResult.OK)
            {
                textBoxPKCS11.Text = dlgOpen.FileName;
            }
        }
    }
}