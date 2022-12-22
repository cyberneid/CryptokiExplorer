using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CryptokiExplorer
{
    public partial class ObjectBrowser : Form
    {
        public ObjectBrowser(object obj)
        {
            InitializeComponent();
            try
            {                
                propertyGrid.SelectedObject = obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        
    }
}