using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sueca_MVC_v3
{
    public partial class UserForm : Form
    {
        private string nome;

        public string Nome { get { return nome; } }

        public UserForm()
        {
            InitializeComponent();
            this.btnConfirmar.Enabled = false;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.nome = this.txtNome.Text;
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (this.txtNome.Text.Trim() != "")
            {
                this.btnConfirmar.Enabled = true;
            }
            else
                this.btnConfirmar.Enabled = false;
        }

    }
}
