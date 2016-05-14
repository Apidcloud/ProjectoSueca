using System;
using System.Windows.Forms;
using Sueca_MVC_v3.Views;
using Sueca_MVC_v3.Models;
using Sueca_MVC_v3.Controllers;

namespace Sueca_MVC_v3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameModel gameModel = new GameModel();

            View view = new View();
            view.Model = gameModel;

            //Table tableView = new Table();
            //MainForm MainFormView = new MainForm(gameModel, tableView);

            GameController gameController = new GameController();

            //tableView.Model = gameModel;
            gameController.Model = gameModel;
            gameController.view = view;
            //gameController.TableView = tableView;
            //gameController.MainView = MainFormView;

            Application.Run(view);
        }
    }
}
