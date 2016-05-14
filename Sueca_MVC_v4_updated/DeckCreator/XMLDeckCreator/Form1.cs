using SuecaLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Sueca_MVC_v3.Models;

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
            SerializeData.Binary.SerializeObject(fileName + ".bin", createDeck());
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

        private List<Carta> createBaralho()
        {
            List<Carta> deck = new List<Carta>();
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
            return deck;
        }

        


        private void btnLoad_Click(object sender, EventArgs e)
        {
            Baralho deck = (Baralho)(SerializeData.Binary.DeserializeObject(fileName + ".bin"));
            if (deck != null)
            {
                // log file
                this.saveLog(deck, "load");
                MessageBox.Show("Load Completed");
            }
            else
                MessageBox.Show("File doesn't exist");
        }

        

        private void btnCreateXML_Click(object sender, EventArgs e)
        {
            SerializeToXML(fileName, createBaralho());
            MessageBox.Show("Finished serializing");
        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            List<Carta> deck = (List<Carta>)(DeserializeFromXML(fileName + ".xml"));
            if (deck != null)
            {
                MessageBox.Show("Load Completed");
            }
            else
                MessageBox.Show("File doesn't exist");
        }


        public static void SerializeToXML(string filename, List<Carta> o)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Carta>));
            TextWriter textWriter = new StreamWriter(filename + ".xml");
            serializer.Serialize(textWriter, o);
            textWriter.Close();
        }

        public static List<Carta> DeserializeFromXML(string filename)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Carta>));
            TextReader textReader = new StreamReader(filename);
            List<Carta> ob;
            ob = (List<Carta>)deserializer.Deserialize(textReader);
            textReader.Close();
            return ob;
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
