using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CryptokiExplorer
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            linkLabel.Links[0].LinkData = "http://www.Cyberneid.com";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // LinkData property of the Link object.
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            System.Diagnostics.Process.Start(target);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}