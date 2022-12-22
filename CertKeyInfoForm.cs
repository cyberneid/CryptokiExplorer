using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CryptokiExplorer
{
    public partial class CertKeyInfoForm : Form
    {
        public CertKeyInfoForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(textBoxLabel.Text.Equals("") ||                
               textBoxID.Text.Equals(""))      
                MessageBox.Show("Error", "Type the object's Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DialogResult = DialogResult.OK;
        }
    }
}