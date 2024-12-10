using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nonograms
{
    public partial class AddImg : Form
    {
        public AddImg()
        {
            InitializeComponent();
        }

        private void DropBox_DragDrop(object sender, DragEventArgs e)
        {
            var image = e.Data.GetData(DataFormats.FileDrop);
            if (image != null)
            {
                var fileNames = image as string[];
                if (fileNames.Length > 0)
                {
                    this.DropBox.Image = Image.FromFile(fileNames[0]);
                    //this.PictureBox.
                }
            }
        }

        private void DropBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

        }

        private void AddImg_Load(object sender, EventArgs e)
        {
            DropBox.AllowDrop = true;
        }
    }
}
