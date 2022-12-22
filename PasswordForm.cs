using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CryptokiExplorer
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {           
            if(textBoxPassword.Text.Equals(""))      
                MessageBox.Show("Error", "Type the Password", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DialogResult = DialogResult.OK;
        }
    }
}