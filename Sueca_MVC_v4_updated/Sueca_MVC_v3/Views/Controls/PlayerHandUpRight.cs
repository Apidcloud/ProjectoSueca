using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sueca_MVC_v3.Views.Controls
{
    public partial class PlayerHandUpRight : UserControl
    {
        public PlayerHandUpRight()
        {
            InitializeComponent();
        }

        public void ChangeHandCard(int pos, string path)
        {
            foreach (PictureBox pic in this.Controls)
            {
                if (pic.Name.ToString() == "pic" + (pos + 1).ToString())
                {
                    if (path == "")
                    {
                        pic.Image = null;
                        pic.Visible = false;
                    }
                    else if (System.IO.File.Exists(path))
                    {
                        pic.Image = Image.FromFile(path);
                        pic.Visible = true;
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    return;
                }
            }
        }
    }
}
