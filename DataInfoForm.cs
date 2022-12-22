using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CryptokiExplorer
{
    public partial class DataInfoForm : Form
    {
        public DataInfoForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(textBoxLabel.Text.Equals("") ||                
               textBoxApp.Text.Equals(""))      
                MessageBox.Show("Error", "Type the object's Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DialogResult = DialogResult.OK;
        }
    }
}