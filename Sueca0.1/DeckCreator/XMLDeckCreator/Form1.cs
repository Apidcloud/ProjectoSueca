using SuecaLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sueca0._1;

namespace XMLDeckCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string fileName = "deck";

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SerializeData.SerializeObject(fileName + ".bin", createDeck());
            MessageBox.Show("Saved Successfully");
        }

        private Baralho createDeck()
        {
            Baralho deck = new Baralho();
            //               2  3  4  5  6  Q  J  R   7  A
            int[] pontos = { 0, 0, 0, 0, 0, 2, 3, 4, 10, 11 };

            for (int i = 0; i < 40; i++)
            {
                int id = i;
                NAIPE naipe = (NAIPE)(id / 10);
                int rank = id % 10;
                int pontuacao = pontos[id - 10 * (id / 10)];
                Carta carta = new Carta(naipe, pontuacao, id, rank);
                deck.Add(carta);
            }
            // log file
            this.saveLog(deck, "save");

            return deck;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Baralho deck = (Baralho)(SerializeData.DeserializeObject(fileName + ".bin"));
            if (deck != null)
            {
                // log file
                this.saveLog(deck, "load");
                MessageBox.Show("Load Completed");
            }
            else
                MessageBox.Show("File doesn't exist");
        }

        public void saveLog(Baralho deck, string addPath = "")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName + addPath + ".txt", false))
            {
                for (int i = 0; i < 40; i++)
                {
                    file.WriteLine("id: " + deck[i].identificador.ToString());
                    file.WriteLine("rank: " + deck[i].rank.ToString());
                    file.WriteLine("naipe: " + deck[i].naipe.ToString());
                    file.WriteLine("pontos: " + deck[i].pontos.ToString());
                }
            }
        }

    }
}
