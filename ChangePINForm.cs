using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CryptokiExplorer
{
    public partial class ChangePINForm : Form
    {
        public ChangePINForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(!textBoxNewPIN1.Text.Equals(textBoxNewPIN2.Text))
                MessageBox.Show("Error", "New PINs don't match", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if(textBoxNewPIN1.Text.Equals("") || 
               textBoxNewPIN2.Text.Equals("") || 
               textBoxOldPIN.Text.Equals(""))      
                MessageBox.Show("Error", "Type the PIN", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DialogResult = DialogResult.OK;
        }
    }
}