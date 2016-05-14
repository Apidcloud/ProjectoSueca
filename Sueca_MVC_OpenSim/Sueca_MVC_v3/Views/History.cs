using Sueca_MVC_v3.Models;
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
    public partial class History : Form
    {
        private Partida partida;

        public History()
        {
            InitializeComponent();
        }

        public History(Partida partida)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.partida = partida;
            showList();
        }

        private void showList()
        {
            this.listPlays.Items.Clear();
            for (int i = 0; i < partida.vazas.Count-1; i++)
            {
                int n = i+1;
                this.listPlays.Items.Add("Vaza nº " + n);
            }
        }

        private void listPlays_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.listPlays.SelectedIndex;

            Vaza vaza = this.partida.vazas[index];

            foreach (Jogada jogada in vaza.jogadas)
	        {
                int id = jogada.jogador.ID;
                PictureBox pic = this.splitContainer1.Panel2.Controls.Find("pictureBox" + id, false)[0] as PictureBox;
                pic.Image = Image.FromFile("Cartas\\" + jogada.carta.naipe.ToString() + "\\" + jogada.carta.identificador + ".png");
	        }
        }
    }
}
