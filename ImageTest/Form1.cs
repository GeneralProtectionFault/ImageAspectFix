using ImageAspectFix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTest
{
    public partial class ImageAspectFixTestWindow : Form
    {
        public ImageAspectFixTestWindow()
        {
            InitializeComponent();
        }



        private void btnProcess_Click(object sender, EventArgs e)
        {
            var imagePath = txtImagesPath.Text.Trim();

            foreach(var file in Directory.GetFiles(imagePath))
            {
                SumoImage image = new SumoImage();

                try
                {
                    image = AspectFix.GrabImageAttributes(file);
                }
                catch (Exception ex)
                {
                    txtLog.AppendText($"Error creating image object for {file} - {ex.Message}\n");
                    continue;
                }


                try
                {
                    AspectFix.FixImage(true, image, file);
                    txtLog.AppendText($"{file} processed.\n");
                }
                catch (Exception ex)
                {
                    txtLog.AppendText($"Error fixing image ration for {file} - {ex.Message}\n");
                }
            }
        }
    }
}
