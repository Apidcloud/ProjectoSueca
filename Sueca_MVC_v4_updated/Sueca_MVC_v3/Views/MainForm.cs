using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sueca_MVC_v3.Views
{
    public partial class MainForm : Form
    {
        #region Events
        public event StringEventHandler AskGameStart;
        public event EmptyEventHandler AskToInitializeData;
        public event StringEventHandler AskToLoadSavedData;
        #endregion

        private Models.GameModel gameModel = null;
        private Views.Table tableView = null;

        public MainForm(Models.GameModel model, Views.Table table)
        {
            InitializeComponent();
            gameModel = model;
            tableView = table;
            model.AnswerGameStart += model_AnswerGameStart;
        }

        void model_AnswerGameStart()
        {
            this.Hide();
            this.tableView.Owner = this;
            this.tableView.Show();
        }

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            if (user.ShowDialog() == DialogResult.OK)
            {
                if (AskGameStart != null)
                {
                    AskGameStart(user.Nome);
                }
                if (AskToInitializeData != null)
                {
                    AskToInitializeData();
                }
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonCarregar_Click(object sender, EventArgs e)
        {
            if (AskToLoadSavedData != null)
            {
                OpenFileDialog old = new OpenFileDialog();
                old.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (old.ShowDialog() != DialogResult.OK) return;
                AskToLoadSavedData(old.FileName.ToString());
            }
        }

    }
}
