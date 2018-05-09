using ImageAspectFix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Just to force the method to run so we can look at the variables
            SumoImage test = new SumoImage();
            AspectFix.FixImage(test, @"C:\");
        }
    }
}
