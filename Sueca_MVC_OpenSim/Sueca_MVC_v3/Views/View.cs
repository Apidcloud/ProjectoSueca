using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using OpenMetaverse;
using Sueca_MVC_v3.Views;
using Sueca_MVC_v3.Models;
using Sueca_MVC_v3.Controllers;

namespace Sueca_MVC_v3
{
    public partial class View : Form
    {
        public event StringDelegate UserIniciaJogo;
        public event EmptyDelegate UserTerminaJogo;
        public event CartaDelegate UserEscolheuCarta;
        public event StringEventHandler SaveGame;
        public event StringEventHandler AskToLoadSavedData;

        Dictionary<string, UUID> elementosView = new Dictionary<string, UUID>();
        //Dictionary<string, UUID> cartasView = new Dictionary<string, UUID>();

        GameModel _mesa = null;

        public GameModel Model
        {
            set
            {
                _mesa = value;
                _mesa.AnswerGameStart += _mesa_JogoIniciado;
                _mesa.VazaChanged += _mesa_AtualizaVaza;
                _mesa.EnableActivePlayer += _mesa_AtualizaJogadorAtivo;
                _mesa.AnswerInitializeData += _mesa_AnswerInitializeData;
                _mesa.ChangePoints += model_ChangePoints;
                _mesa.ChangeGamePoints += model_ChangeGamePoints;
            }
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
            
        }

        void model_ChangeGamePoints(int a, int b)
        {
            string msg = "";
            msg = "Partida= " + _mesa.partidas[_mesa.curPartida].Equipas[a].pontosPartida + " Total= " + _mesa.partidas[_mesa.curPartida].Equipas[a].pontosJogo;
            if (elementosView.ContainsKey("Pontos" + a))
                cliente.Self.Chat(elementosView["Pontos" + a].ToString() + ":" + msg, -182, ChatType.Normal);

            
            AtualizaHistorico(0);
        }

        void model_ChangePoints(int a, int b)
        {
            model_ChangeGamePoints(a, b);
        }

        void _mesa_AnswerInitializeData()
        {
            for (int i = 0; i < 4; i++)
            {
                Models.IPlayer player = _mesa.partidas[_mesa.curPartida].Equipas[i % 2].jogadores[i / 2];

                player.RefreshHand += _AtualizaCartasMao;
            }
        }

        # region Open SIM
        Dictionary<UUID, uint> mergeObjectIDAndLocalID = new Dictionary<UUID, uint>();
        static AutoResetEvent  gotMyObject = new AutoResetEvent(false);

        // make sure we are referencing the root object of the group			
        bool gotKillObject = false;
        bool gotObjectInventory = false;
        UUID newInventoryItemID = UUID.Zero;
        AutoResetEvent objectInInventoryWait = new AutoResetEvent(false);
        AutoResetEvent objectKillWait = new AutoResetEvent(false);

        static Dictionary<UUID, Primitive> primsWaiting = new Dictionary<UUID, Primitive>();
        static AutoResetEvent allPropertiesReceived = new AutoResetEvent(false);

        string caminhoCredenciais = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\UTAD\\DemoSL_Simplificado";

        GridClient cliente = new GridClient();

        InventoryItem objectoDoInventario = null;

        string nomeDoObjectoColocado = "PlacarJogo";

        uint objectoColocado = uint.MinValue;

        public View()
        {
            InitializeComponent();

            cliente.Settings.LOGIN_SERVER = "http://127.0.0.1:9000/";
            cliente.Network.Disconnected += new EventHandler<DisconnectedEventArgs>(Network_Disconnected);
            cliente.Self.ChatFromSimulator += new EventHandler<ChatEventArgs>(Self_ChatFromSimulator);
            cliente.Self.IM += new EventHandler<InstantMessageEventArgs>(Self_IM);
            cliente.Network.LoginProgress += new EventHandler<LoginProgressEventArgs>(Network_LoginProgress);
        }

        void Network_LoginProgress(object sender, LoginProgressEventArgs e)
        {
            if (e.Status == LoginStatus.Success)
            {
                registaLog("Login efectuado");
                ActualizaForm("Ligado");

                UUID objectFolderID = cliente.Inventory.FindFolderForType(AssetType.Object);
                List<InventoryBase> currentContents = cliente.Inventory.FolderContents(objectFolderID, cliente.Self.AgentID, false, true, InventorySortOrder.ByName, 60000);

                if (currentContents != null)
                {
                    if (currentContents.Count > 0)
                    {
                        registaLog("Objectos no Inventário:");
                        foreach (InventoryBase b in currentContents)
                        {
                            InventoryItem item = b as InventoryItem;
                            registaLog("»» " + item.Name);
                            if (item.Name == nomeDoObjectoColocado)
                                objectoDoInventario = item;
                        }
                        if (objectoDoInventario != null)
                        {
                            RezInventoryItem(cliente, objectoDoInventario);
                            if (objectoColocado == uint.MinValue)
                                registaLog("Objecto inicial não colocado...");
                            else
                                registaLog("Objecto inicial (LocalID) : " + objectoColocado.ToString());
                        }
                    }
                    else
                        registaLog("Não contém objectos no Inventário.");
                }
                else
                    registaLog("Erro na obtenção do Inventário.");

                //UUID inventoryFolderID = cliente.Inventory.FindFolderForType(AssetType.Texture);
                //foreach (Naipe np in Enum.GetValues(typeof(Naipe)))
                //{
                //    for (int i = 0; i < 10; i++)
                //    {
                //        string tmp = np.ToString() + i;
                //        cartasView.Add(tmp, new UUID(cliente.Inventory.FindObjectByPath(inventoryFolderID, cliente.Self.AgentID, tmp, 6000)));
                //    }
                //}
            }
        }

        #region RECEBEMENSAGEM
        void Self_IM(object sender, InstantMessageEventArgs e)
        {
             // recebe uma mensagem proveniente do objecto colocado
            ActualizaIM(e.IM.Message);
            string mensagem = e.IM.Message;
            string[] partes = mensagem.Split(':');
            switch (partes[0])
            {
                case "ID":
                    elementosView.Add(partes[1], new UUID(partes[2]));
                    break;
                case "CMD":
                    switch (partes[1])
                    {
                        case "UserQuit":
                            if (UserTerminaJogo != null)
                                UserTerminaJogo();
                            break;
                        case "UserStart":
                            if(UserIniciaJogo!=null)
                                UserIniciaJogo(partes[2]);
                            break;
                        case "UserChoose":
                            if (UserEscolheuCarta != null)
                                UserEscolheuCarta(Convert.ToInt32(partes[2]));
                            break;
                    }
                    break;
                case "ESQ":
                    AtualizaHistorico(-1);
                    break;
                case "DIR":
                    AtualizaHistorico(1);
                    break;
                case "SAVE":
                    if (SaveGame != null)
                    {
                        SaveGame("filename.xml");
                    }
                    cliente.Self.Chat("Salvo com Sucesso", 0, ChatType.Normal);
                   
                    break;
                case "OPEN":
                    if (AskToLoadSavedData != null)
                    {
                        AskToLoadSavedData("filename.xml");
                    }
                    cliente.Self.Chat("Aberto com Sucesso", 0, ChatType.Normal);
                  
                  
                    break;

                    
            }
        }
        #endregion
        int historicoAtual = -1;   
        void AtualizaHistorico(int incremento) { //-1 esquerda 1 direita 
            if (incremento == 0){
                for (int i = 0; i < 4; i++)
                    cliente.Self.Chat(elementosView["historico" + i].ToString() + ":disable", -182, ChatType.Normal);
                historicoAtual = -1;   
            }
            else if(historicoAtual+incremento<_mesa.partidas[_mesa.curPartida].vazas.Count-1 && historicoAtual+incremento >= 0)
            {
                historicoAtual += incremento;
                foreach (Jogada jogada in _mesa.partidas[_mesa.curPartida].vazas[historicoAtual].jogadas)
                {
                    Carta c = jogada.carta;
                    if (c == null)
                        cliente.Self.Chat(elementosView["historico" + jogada.jogador.ID].ToString() + ":disable", -182, ChatType.Normal);
                    else
                        cliente.Self.Chat(elementosView["historico" + jogada.jogador.ID].ToString() + ":" + c.naipe + "" + c.rank, -182, ChatType.Normal);

                }
            }
        }

        void Self_ChatFromSimulator(object sender, ChatEventArgs e)
        {
             // filtrar as minhas conversas e as mensagens vazias
            if (e.FromName != cliente.Self.Name && e.Message != "")
            {
                registaLog("[" + e.FromName + "] " + e.Message);
            }
        }

        void Network_Disconnected(object sender, DisconnectedEventArgs e)
        {
            registaLog("Desligado : Motivo = " + e.Reason);
            ActualizaForm("Desligado");
        }

        public delegate void MyChatDelegate(string msg, string deQuem);

        public event MyChatDelegate RecebiMensagemChat;

        private void ActualizaIM(string mensagem)
        {
            if (labelIM.InvokeRequired == true)
                labelIM.Invoke(new XPTO(this.ActualizaIM), new object[] { mensagem });
            else
                labelIM.Text = "IM recebida: " + mensagem;
        }
    
        private void ActualizaInfo(string modo)
        {
            if (labelBot.InvokeRequired == true)
                labelBot.Invoke(new XPTO(this.ActualizaInfo), new object[] { modo });
            else
            {
                if (modo == "on")
                {
                    labelBot.Text = "Bot: " + cliente.Self.Name;
                    labelLocal.Text = "Região: " + cliente.Network.CurrentSim.Name + ": " + cliente.Self.RelativePosition.ToString();
                }
                else
                {
                    labelBot.Text = "Bot: ";
                    labelLocal.Text = "Região: ";
                }
            }
        }

        private void View_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(caminhoCredenciais) == false)
            {
                Directory.CreateDirectory(caminhoCredenciais);
            }
            else
            {
                if (File.Exists(caminhoCredenciais + "\\pass.txt") == true)
                {
                    StreamReader str = new StreamReader(caminhoCredenciais + "\\pass.txt");
                    string linha = "";
                    while ((linha = str.ReadLine()) != null)
                    {
                        switch (linha.Split('=')[0])
                        {
                            case "Nome":
                                textBoxNome.Text = linha.Split('=')[1];
                                break;
                            case "Sobrenome":
                                textBoxSobrenome.Text = linha.Split('=')[1];
                                break;
                            case "Password":
                                textBoxPassword.Text = linha.Split('=')[1];
                                break;
                        }
                    }
                    str.Close();
                    checkBoxGuardar.Checked = true;
                }
            }
            ActualizaForm("Desligado");
        }
        
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // guardar as credenciais em ficheiro
            if (checkBoxGuardar.Checked == true)
            {
                StreamWriter str = new StreamWriter(caminhoCredenciais + "\\pass.txt");
                str.WriteLine("Nome=" + textBoxNome.Text);
                str.WriteLine("Sobrenome=" + textBoxSobrenome.Text);
                str.WriteLine("Password=" + textBoxPassword.Text);
                str.Close();
            }
            // efectuar login no SL

            if (cliente.Network.Connected == true)
                cliente.Network.Logout();

            ActualizaForm("Espera");

            cliente.Network.Login(
                textBoxNome.Text,
                textBoxSobrenome.Text,
                textBoxPassword.Text,
                "MyBot",
                "0.1"
                );
            // desloca o Bot para uma região específica
            cliente.Self.Teleport("gian", new Vector3(124, 120, 25));
            // comunica com a comunidade presente (em chat publico)
            cliente.Self.Chat("Sueca MVC", -182, ChatType.Normal);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (cliente.Network.Connected == true)
            {
                if (objectoColocado != uint.MinValue)
                {
                    UUID trashFolderID = cliente.Inventory.FindFolderForType(AssetType.TrashFolder);
                    DeRezObject(cliente, objectoColocado, DeRezDestination.TrashFolder, trashFolderID);
                }
                cliente.Network.Logout();
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            buttonLogout_Click(sender, e);
            Application.Exit();
        }

        private void ActualizaForm(string estado)
        {
            if (buttonLogout.InvokeRequired == true)
                buttonLogout.Invoke(new XPTO(this.ActualizaForm), new object[] { estado });
            else{
                switch (estado)
                {
                    case "Ligado":
                        buttonLogout.Enabled = true;
                        buttonLogin.Enabled = false;
                        textBoxNome.Enabled = false;
                        textBoxSobrenome.Enabled = false;
                        textBoxPassword.Enabled = false;
                        checkBoxGuardar.Enabled = false;
                        this.Cursor = Cursors.Arrow;
                        ActualizaInfo("on");
                        break;
                    case "Desligado":
                        buttonLogout.Enabled = false;
                        buttonLogin.Enabled = true;
                        textBoxNome.Enabled = true;
                        textBoxSobrenome.Enabled = true;
                        textBoxPassword.Enabled = true;
                        checkBoxGuardar.Enabled = true;
                        this.Cursor = Cursors.Arrow;
                        ActualizaInfo("off");
                        break;
                    case "Espera":
                        buttonLogout.Enabled = false;
                        buttonLogin.Enabled = false;
                        textBoxNome.Enabled = false;
                        textBoxSobrenome.Enabled = false;
                        textBoxPassword.Enabled = false;
                        checkBoxGuardar.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;
                        break;
                }
            }
        }

        #region ENVIAMENSAGEMAOOPENSIM
        public void EnviaMensagem(string texto)
        {
            //cliente.Self.Chat(texto, -182, ChatType.Normal);
            registaLog("[Resposta] " + texto); 
        }
        #endregion

        private void registaLog(string linhaTexto)
        {
            if (textBoxLogs.InvokeRequired == true)
                textBoxLogs.Invoke(new XPTO(registaLog), new object[] { linhaTexto });
            else
                textBoxLogs.Text += linhaTexto + Environment.NewLine;
        }

        // delegado para executar os métodos de "provocam" cross-reference 
        private delegate void XPTO(string msg);

        // ################################################################################
        // ########             rotinas importadas/adaptadas da NET            ############
        // ################################################################################

        private void RezInventoryItem(GridClient cliente, InventoryItem inventoryItem)
        {
            mergeObjectIDAndLocalID = new Dictionary<UUID, uint>();
            gotMyObject = new AutoResetEvent(false);

            EventHandler<ObjectPropertiesEventArgs> opc =new EventHandler<ObjectPropertiesEventArgs>(Objects_ObjectProperties);
            cliente.Objects.ObjectProperties += opc;

            EventHandler<PrimEventArgs> opn = new EventHandler<PrimEventArgs>(Objects_ObjectUpdate);
            cliente.Objects.ObjectUpdate += opn;
            
            // next rez the object
            registaLog("Vou fixar o objecto " + inventoryItem.Name);
            cliente.Inventory.RequestRezFromInventory(cliente.Network.CurrentSim,
                                                                 new Quaternion(0.0f, 0.0f, 0.0f, 0.0f),
                                                                 new Vector3(cliente.Self.RelativePosition.X,
                                                                              cliente.Self.RelativePosition.Y,
                                                                              cliente.Self.RelativePosition.Z),
                                                                 inventoryItem,
                                                                 cliente.Self.ActiveGroup,
                                                                 UUID.Random(),
                                                                 true);

            // finally wait until we know it has been rez'ed (or give up after 60 seconds)
            bool found = gotMyObject.WaitOne(60 * 1000);
            // stop listening for the events again

            cliente.Objects.ObjectProperties -= opc;
            cliente.Objects.ObjectUpdate -= opn;
        }

        void Objects_ObjectUpdate(object sender, PrimEventArgs e)
        {
            if (e.IsNew)
            {
                // if the new object is already registered, then it must have
                // come from the ObjectProperties event, and we now know both.
                if (mergeObjectIDAndLocalID.ContainsKey(e.Prim.ID))
                {
                    objectoColocado = e.Prim.LocalID;
                    gotMyObject.Set();
                }
                //else
                //{
                //    // workaround for a bug in OpenSim, where you cannot trust the owner id of the first
                //    // object properties package, by selecting it we force another object properties package
                //    // to be sent
                //    registaLog("BUG???");
                //    cliente.Objects.SelectObject(cliente.Network.CurrentSim, prim.LocalID);
                //}
               // mergeObjectIDAndLocalID[e.Prim.ID] = e.Prim.LocalID;
            }
        }

        void Objects_ObjectProperties(object sender, ObjectPropertiesEventArgs e)
        {
            if (e.Properties.OwnerID == cliente.Self.AgentID && e.Properties.Name == nomeDoObjectoColocado)
            {
                registaLog("Já chegaram as propriedades do objecto que coloquei");
                // if new object already registered this object, then it must be one
                // of the objects we've rezzed, as it contains the right owner id
                if (mergeObjectIDAndLocalID.ContainsKey(e.Properties.ObjectID))
                {
                    registaLog("Já está...");
                    objectoColocado = mergeObjectIDAndLocalID[e.Properties.ObjectID];
                    gotMyObject.Set();
                }
                else
                {
                    registaLog("Ainda não chegou o PRIM ???...");
                    mergeObjectIDAndLocalID[e.Properties.ObjectID] = uint.MinValue;
                }
            }
            else
                registaLog("Népias : " + e.Properties.Name);
        }

        public void DeRezObject(GridClient cliente, uint rootLocalId, DeRezDestination destinationType, UUID destinationFolder)
        {
            // wait for the kill object packet, otherwise we risk logging out before everything is in place
            objectKillWait = new AutoResetEvent(false);
            newInventoryItemID = UUID.Zero;
            objectInInventoryWait = new AutoResetEvent(false);

            EventHandler<TaskItemReceivedEventArgs>HandleOnTaskItemReceived = new EventHandler<TaskItemReceivedEventArgs>(Inventory_TaskItemReceived);
            EventHandler<KillObjectEventArgs> kic = new EventHandler<KillObjectEventArgs>(Objects_KillObject);

            cliente.Objects.KillObject += kic;
            cliente.Inventory.TaskItemReceived+=HandleOnTaskItemReceived;
            cliente.Objects.RequestObject(cliente.Network.Simulators[0], rootLocalId);
            cliente.Inventory.RequestDeRezToInventory(rootLocalId, destinationType, destinationFolder, UUID.Random());

            //bool processCompleted = WaitHandle.WaitAll(new WaitHandle[] { objectKillWait, objectInInventoryWait }, 20 * 1000, true);
            bool processCompleted1 = WaitHandle.WaitAll(new WaitHandle[] { objectKillWait }, 20 * 1000, true);
            bool processCompleted2 = WaitHandle.WaitAll(new WaitHandle[] { objectInInventoryWait }, 20 * 1000, true);

            cliente.Objects.KillObject -= kic;
            cliente.Inventory.TaskItemReceived -= HandleOnTaskItemReceived;
            if (!processCompleted1 || !processCompleted2)
            {
                if (!gotObjectInventory)
                    registaLog("Falhou a remoção do objecto " + nomeDoObjectoColocado + ", excedeu o temp enquanto esperava pela confirmação do ammazenamento no inventário");
                if (!gotKillObject)
                    registaLog("Falhou a remoção do objecto " + nomeDoObjectoColocado + ", o objecto não foi removido do cenário");
            }
        }

        void Objects_KillObject(object sender, KillObjectEventArgs e)
        {
            if (e.ObjectLocalID == objectoColocado)
            {
                gotKillObject = true;
                objectKillWait.Set();
            }
        }

        void Inventory_TaskItemReceived(object sender, TaskItemReceivedEventArgs e)
        {
            // TODO, be a little more selective in when to accept the result
            //if (rezedPrimitive.Properties.CreatorID == creatorID)
            if (e.CreatorID == cliente.Self.AgentID)
            {
                gotObjectInventory = true;
                newInventoryItemID = e.ItemID;
                objectInInventoryWait.Set();
            }
        }

        # endregion

        private void _mesa_AtualizaJogadorAtivo()
        {
            for (int n = 0; n < 4; n++)
            {
                Models.IPlayer player = _mesa.partidas[_mesa.curPartida].Equipas[n % 2].jogadores[n / 2];
             
                if (player is BotPlayer)
                {
                    if (_mesa.ActivePlayerIndex == n)
                    {
                        if (elementosView.ContainsKey("QuantasCartas" + n))
                            cliente.Self.Chat(elementosView["QuantasCartas" + n].ToString() + ":ON", -182, ChatType.Normal);
                    }
                    else
                    {
                        if (elementosView.ContainsKey("QuantasCartas" + n))
                            cliente.Self.Chat(elementosView["QuantasCartas" + n].ToString() + ":OFF", -182, ChatType.Normal);
                    }
                }
            }
        }

     

        private void _mesa_AtualizaVaza()
        {
           

            if (_mesa.partidas[_mesa.curPartida].vazas[_mesa.partidas[_mesa.curPartida].vazas.Count - 1].getNumeroJogadas() == 0)
            
                for (int i = 0; i < 4; i++) 
                    cliente.Self.Chat(elementosView["Vaza" + i].ToString() + ":disable", -182, ChatType.Normal);
                
            else{

                foreach (Jogada jogada in _mesa.partidas[_mesa.curPartida].vazas[_mesa.partidas[_mesa.curPartida].vazas.Count - 1].jogadas)
                {
                    Carta c = jogada.carta;
                     
                    if(c==null)
                            cliente.Self.Chat(elementosView["Vaza" + jogada.jogador.ID ].ToString() + ":disable", -182, ChatType.Normal);
                    else
                        cliente.Self.Chat(elementosView["Vaza" + jogada.jogador.ID].ToString() + ":" + c.naipe + "" + c.rank, -182, ChatType.Normal);
                   
                }
            }

        }

        void _mesa_JogoIniciado()
        {
          
            int i = 0;
            string nomeCompleto = "";
             for ( i = 0; i < 4; i++)
            {
                Models.IPlayer jog = _mesa.partidas[_mesa.curPartida].Equipas[i % 2].jogadores[i / 2];
             
                if(jog is BotPlayer)
                    nomeCompleto = "Bot - " + jog.Nome;

                if(elementosView.ContainsKey("Nome" + i))
                    cliente.Self.Chat(elementosView["Nome" + i].ToString() + ":" + nomeCompleto, -182, ChatType.Normal);

               
            }
        }

        private void _AtualizaCartasMao(IPlayer jogador)
        {
            int n=0;
            for ( n = 0; n < 4; )
            {
                Models.IPlayer jog = _mesa.partidas[_mesa.curPartida].Equipas[n % 2].jogadores[n/ 2];
            
                if (jog.Nome == jogador.Nome)
                    break;
                n++;
            }
            if (jogador is BotPlayer)
            {
                cliente.Self.Chat(elementosView["QuantasCartas" + n].ToString() + ":" + jogador.ContaCartas(), -182, ChatType.Normal);
                return;
            }

            int i = 0;
            foreach (Carta c in jogador.Mao) {
                if (elementosView.ContainsKey("Carta" + i) == false)
                    break;
                if(c == null)
                    cliente.Self.Chat(elementosView["Carta" + i].ToString() + ":disable", -182, ChatType.Normal);
                else
                    cliente.Self.Chat(elementosView["Carta" + i].ToString() + ":" + c.naipe + "" + c.identificador % 10, -182, ChatType.Normal);

                i++;
            }


            Carta trunfo = _mesa.partidas[_mesa.curPartida].trunfo;
            cliente.Self.Chat(elementosView["Trunfo"].ToString()+":"+trunfo.naipe+""+trunfo.identificador%10, -182, ChatType.Normal);

        }
    }
}
