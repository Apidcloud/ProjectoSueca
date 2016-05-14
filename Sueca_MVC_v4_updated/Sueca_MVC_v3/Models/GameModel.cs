using Sueca_MVC_v3.Controllers;
using SuecaLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Sueca_MVC_v3.Models
{
    public class GameModel
    {
        #region Events
        public event EmptyEventHandler AnswerGameStart;
        public event EmptyEventHandler AnswerInitializeData;
        public event EmptyEventHandler CardsSet;
        public event EmptyEventHandler VazaChanged;
        public event EmptyEventHandler EnableActivePlayer;
        public event IntIntEventHandler ChangePoints;
        public event IntIntEventHandler ChangeGamePoints;
        #endregion

        [XmlIgnore]
        public Baralho deck { get; private set; }

        [XmlIgnore]
        public Team[] equipas { get; private set; }

        [XmlIgnore]
        public IPlayer[] players { get; private set; }

        public List<Partida> partidas { get; private set; }

        public string UserName { get; private set; }


        public int nextPartidaPlayer { get; private set; }

        private int activePlayer = 0;

        public int curPartida { get; private set; }

        public IPlayer ActivePlayer
        {
            get { return players[activePlayer]; }
        }

        public int ActivePlayerIndex
        {
            get { return activePlayer; }
        }
        

        public void GameStart(string text)
        {
            this.UserName = text;
            if (AnswerGameStart != null)
            {
                AnswerGameStart();
            }
        }

        public void ReadDeck(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                //try
                //{
                    this.deck = (Baralho)SerializeData.Binary.DeserializeObject(filename);
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
            }
            else
            {
                throw new FileNotFoundException("Ficheiro do baralho não encontrado");
            }
        }

        private Dictionary<int, List<Carta>> PlayerHandLists = new Dictionary<int, List<Carta>>();

        public void InitializeSavedData(string filename)
        {
            ReadDeck("deck.bin");
            DeserializeFromXML(filename);
            curPartida = 0;
            if (AnswerInitializeData != null)
            {
                AnswerInitializeData();
            }
            for (int i = 0; i < 4; i++)
            {
                this.equipas[i % 2].jogadores[i / 2].ReceberComNulas(PlayerHandLists[i]);
            }
            if (CardsSet != null)
            {
                CardsSet();
            }
            for (int i = 0; i < 2; i++)
            {
                if (ChangePoints != null)
                {
                    ChangePoints(i, this.equipas[i].pontosPartida);
                }
                if (ChangeGamePoints != null)
                {
                    ChangeGamePoints(i, this.equipas[i].pontosJogo);
                }
            }
            
            if (activePlayer != 0)
            {
                GameController.timer.Start();
            }
        }

        public void InitializeData()
        {
            this.nextPartidaPlayer = 0;
            this.curPartida = 0;
            //... inicializar jogadores, equipas, partida...
            gerarEquipas(this.UserName);
            this.players = new IPlayer[4];
            for (int i = 0; i < 4; i++)
			{
			    this.players[i] = this.equipas[i % 2].jogadores[i / 2];
			}
            this.partidas = new List<Partida>();
            this.partidas.Add(new Partida(this.deck, this.equipas, 0));
            
            if (AnswerInitializeData != null) 
            {
                AnswerInitializeData();
            }

            this.partidas[0].darCartas();
            this.partidas[0].vazas.Add(new Vaza());

            if (CardsSet != null)
            {
                CardsSet();
            }
        }



        public void givePoints()
        {
            int ponto = 1;
            int equipa = 0;

            if (this.equipas[1].pontosPartida > 60)
                equipa = 1;

            if (this.equipas[equipa].pontosPartida == 120)
                ponto = 4;
            else if (this.equipas[equipa].pontosPartida > 90)
                ponto = 2;
            else if (this.equipas[equipa].pontosPartida == 60)
                ponto = 0;

            this.equipas[equipa].pontosJogo += ponto;

            if (ChangeGamePoints != null && ponto != 0)
            {
                ChangeGamePoints(equipa, this.equipas[equipa].pontosJogo);
            }
        }

        public void createNewPartida()
        {
            if (this.partidas != null)
            {
                // limpar pontos partida
                for (int i = 0; i < 2; i++)
                {
                    this.equipas[i].pontosPartida = 0;
                    ChangePoints(i, 0);
                }
                this.activePlayer = (++this.nextPartidaPlayer) % 4;
                this.curPartida += 1;
                this.partidas.Add(new Partida(this.deck, this.equipas, 0));
                this.partidas[curPartida].darCartas();
                this.partidas[curPartida].vazas.Add(new Vaza());
                if (EnableActivePlayer != null)
                {
                    EnableActivePlayer();
                }
                if (CardsSet != null)
                {
                    CardsSet();
                }
            }
        }

        private void gerarEquipas(string nomeJogadorPrincipal)
        {
            List<IPlayer> jogadores = new List<IPlayer>();
            jogadores.Add(new HumanPlayer() { Nome = nomeJogadorPrincipal, ID = 0});
            
            jogadores.Add(new BotPlayer() {Nome = "Parceiro Bot", ID = 1});
            this.equipas = new Team[2];
            this.equipas[0] = new Team(jogadores);
            jogadores = new List<IPlayer>();
            jogadores.Add(new BotPlayer() {Nome = "Bot 2", ID = 2});
            jogadores.Add(new BotPlayer() {Nome = "Bot 3", ID = 3});
            this.equipas[1] = new Team(jogadores);
            jogadores = null;
        }

        public void NextPlayer()
        {
            // já jogaram todos?
            if (VazaComplete() && this.partidas[curPartida].vazas.Count <= 10)
            {
                // decide quem joga a próxima
                IPlayer p = VazaWin(this.partidas[this.curPartida].vazas[this.partidas[this.curPartida].vazas.Count - 1], this.partidas[this.curPartida].trunfo.naipe);
                for (int i = 0; i < 4; i++)
			    {
			        if (players[i].ID == p.ID)
	                {
		                activePlayer = i;
                        break;
	                }
			    }
                if (activePlayer == null)
                {
                    //throw new Invalid
                }
                this.partidas[this.curPartida].vazas.Add(new Vaza());
                if (activePlayer >= 4)
                    activePlayer = 0;
                if (VazaChanged != null)
                    VazaChanged();
            }
            else
            {
                activePlayer++;
                if (activePlayer >= 4)
                    activePlayer = 0;
            }

            if (EnableActivePlayer != null)
            {
                EnableActivePlayer();
            }
        }

        public bool VazaComplete()
        {
            return this.partidas[this.curPartida].vazas[partidas[this.curPartida].vazas.Count - 1].getNumeroJogadas() == 4;
        }

        private IPlayer VazaWin(Vaza vaza, NAIPE trunfo)
        {
            if (VazaComplete())
            {
                NAIPE naipeVaza = vaza.jogadas[0].carta.naipe;
                Jogada jogadaMaior = vaza.jogadas[0];
                int pontos = 0;
                foreach (Jogada jogada in vaza.jogadas)
                {
                    pontos += jogada.carta.pontos;

                    if (jogada.carta.naipe == trunfo)
                    {
                        naipeVaza = trunfo;
                    }
                    if (jogadaMaior.carta.naipe != naipeVaza)
                        jogadaMaior = null;

                    if (jogadaMaior == null || (jogada.carta.identificador > jogadaMaior.carta.identificador && naipeVaza == jogada.carta.naipe))
                    {
                        jogadaMaior = jogada;
                    }
                }
                int equipa = 0;
                equipa = (jogadaMaior.jogador.ID == 0 || jogadaMaior.jogador.ID == 1) ? 0 : 1;
                this.equipas[equipa].pontosPartida += pontos;
                if (ChangePoints != null)
                {
                    ChangePoints(equipa, this.equipas[equipa].pontosPartida);
                }
                return jogadaMaior.jogador;
            }
            return null;
        }

        public bool EnableCard(int pos)
        {
            if (partidas[this.curPartida].vazas[partidas[this.curPartida].vazas.Count - 1].getNumeroJogadas() == 0)
                return true;

            NAIPE naipeVaza = partidas[this.curPartida].vazas[partidas[this.curPartida].vazas.Count - 1].jogadas[0].carta.naipe;

            foreach (IPlayer player in this.players)
            {
                if (player is HumanPlayer)
                {
                    if (player.Mao[pos].naipe == naipeVaza)
                    {
                        return true;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        if (player.Mao[i] != null && player.Mao[i].naipe == naipeVaza)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public void PlayCard(IPlayer player, Carta card)
        {
            Jogada jogada = new Jogada(player, card);
            this.partidas[this.curPartida].vazas[partidas[this.curPartida].vazas.Count - 1].jogadas.Add(jogada);
            if (VazaChanged != null)
            {
                VazaChanged();
            }
            if (player is HumanPlayer)
            {
                // so se pode remover a carta depois de mudar a vaza, devido a conflitos de imagens
                HumanPlayer h = this.ActivePlayer as HumanPlayer;
                h.Retirar(h.tempPosicao);
                h.tempPosicao = -1;
            }
            else if (player is BotPlayer)
            {
                BotPlayer h = this.ActivePlayer as BotPlayer;
                h.Retirar(h.tempPosicao);
                h.tempPosicao = -1;
            }
           
        }

        public void SerializeToXML(string filename)
        {
            
            XDocument doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                new XElement("GameModel",
                    new XElement("nextPartidaPlayer", nextPartidaPlayer),
                    new XElement("activePlayer", activePlayer),
                    new XElement("Partida", 
                        new XElement("CartaTrunfo", this.partidas[curPartida].trunfo.identificador),
                        new XElement("numVazas", this.partidas[curPartida].numVazas),
                        new XElement("Vazas"),
                        new XElement("Equipas"))));

            int i = 0;
            foreach (Vaza vaza in partidas[curPartida].vazas)
            {
                XElement newVaza = new XElement("Vaza",
                    new XAttribute("id", i));

                foreach (Jogada jogada in vaza.jogadas)
                {
                    int id = -1;
                    if (jogada.jogador.ID == 0)
                        id = 0;
                    else if (jogada.jogador.ID == 2)
                        id = 1;
                    else if (jogada.jogador.ID == 1)
                        id = 2;
                    else if (jogada.jogador.ID == 3)
                        id = 3;

                    XElement newCarta = new XElement("Carta",
                        new XAttribute("id", jogada.carta.identificador),
                        new XAttribute("jogadorId", id));
                    newVaza.Add(newCarta);
                }
                doc.Element("GameModel").Element("Partida").Element("Vazas").Add(newVaza);
                i++;
            }

            i = 0;
            foreach (Team team in partidas[curPartida].Equipas)
            {
                XElement newTeam = new XElement("Equipa",
                    new XAttribute("id", i),
                    new XAttribute("pontosJogo", partidas[curPartida].Equipas[i].pontosJogo),
                    new XAttribute("pontosPartida", partidas[curPartida].Equipas[i].pontosPartida));
               
                foreach (IPlayer player in team.jogadores)
	            {
                    string isHuman = player is HumanPlayer ? "true" : "false";

                    XElement newPlayer = new XElement("Jogador",
                        new XAttribute("id", player.ID),
                        new XAttribute("human", isHuman),
                        new XElement("Nome", player.Nome),
                        new XElement("tempPosicao", player.tempPosicao),
                        new XElement("Mao"));
                    int n = 1;
                    foreach (Carta carta in player.Mao)
                    {
                        XElement newCarta;
                        if (carta != null)
                        {
                            newCarta = new XElement("Carta",
                            new XAttribute("id", carta.identificador),
                            new XAttribute("posicaoImagem", n));
                        }
                        else
                        {
                             newCarta = new XElement("Carta",
                                new XAttribute("id", -1),
                                new XAttribute("posicaoImagem", n));
                        }
                        newPlayer.Element("Mao").Add(newCarta);
                        n++;
                    }
                    newTeam.Add(newPlayer);
	            }
                doc.Element("GameModel").Element("Partida").Element("Equipas").Add(newTeam);
                i++;
            }


            doc.Save(filename);



        }

        int[] pontosFull = {0,0,0,0,0,2,3,4,10,11};

        public void DeserializeFromXML(string filename)
        {
            XDocument doc = XDocument.Load(filename);

            this.nextPartidaPlayer = Convert.ToInt32(doc.Element("GameModel").Element("nextPartidaPlayer").Value);
            this.activePlayer = Convert.ToInt32(doc.Element("GameModel").Element("activePlayer").Value);
            
            this.equipas = new Team[2];
            this.players = new IPlayer[4];
            List<IPlayer> jogadores = new List<IPlayer>();
            var xmlEquipas = from al in doc.Element("GameModel").Element("Partida").Element("Equipas").Descendants("Equipa") select al;
            int i = 0;
            int n = 0;
            foreach (var newTeam in xmlEquipas)
            {
                var xmlJogadores = from al in newTeam.Descendants("Jogador") select al;
                
                foreach (var newJogador in xmlJogadores)
                {
                    int jogadorID = Convert.ToInt32(newJogador.Attribute("id").Value);
                    var xmlMao = from al in newJogador.Element("Mao").Descendants("Carta") select al;
                    List<Carta> cartasMao = new List<Carta>();
                    foreach (var newCarta in xmlMao)
	                {
                        int id = Convert.ToInt32(newCarta.Attribute("id").Value);
                        Carta carta;
                        if (id != -1)
	                    {
		                    NAIPE naipe = (NAIPE)(id/10);
                            int rank = id % 10;
                            int pontos = pontosFull[id-10*(id/10)];
                            carta = new Carta(naipe, pontos, id, rank);
	                    }
                        else
                        {
                            //carta = new Carta(NAIPE.nenhum, -1, -1, -1);
                            carta = null;
                        }
                        cartasMao.Add(carta);
	                }
                    if (newJogador.Attribute("human").Value == "true")
                    {
                        HumanPlayer human = new HumanPlayer();
                        human.Nome = newJogador.Element("Nome").Value;
                        human.tempPosicao = Convert.ToInt32(newJogador.Element("tempPosicao").Value);
                        //human.Receber(cartasMao);
                        human.ID = jogadorID;
                        //this.players[n] = human;
                        jogadores.Add(human);
                    }
                    else
                    {
                        BotPlayer bot = new BotPlayer();
                        bot.Nome = newJogador.Element("Nome").Value;
                        bot.tempPosicao = Convert.ToInt32(newJogador.Element("tempPosicao").Value);
                        //bot.Receber(cartasMao);
                        bot.ID = jogadorID;
                        //this.players[n] = bot;
                        jogadores.Add(bot);
                    }
                    PlayerHandLists.Add(n, cartasMao);
                    if (jogadores.Count == 2)
	                {
                        this.equipas[i] = new Team(jogadores);
                        this.equipas[i].pontosPartida = Convert.ToInt32(newTeam.Attribute("pontosPartida").Value);
                        this.equipas[i].pontosJogo = Convert.ToInt32(newTeam.Attribute("pontosJogo").Value);
                        jogadores = new List<IPlayer>();
	                }
                    n++;
                }
                i++;
            }
            for (int j = 0; j < 4; j++)
            {
                this.players[j] = this.equipas[j % 2].jogadores[j / 2];
            }
            Partida partida = new Partida(deck, equipas, 0);
            int trunfoID = Convert.ToInt32(doc.Element("GameModel").Element("Partida").Element("CartaTrunfo").Value);
            int numVazas = Convert.ToInt32(doc.Element("GameModel").Element("Partida").Element("numVazas").Value);
            NAIPE naipeTrunfo = (NAIPE)(trunfoID/10);
            int rankTrunfo = trunfoID % 10;
            int pontosTrunfo = trunfoID-10*(trunfoID/10);
            Carta cartaTrunfo = new Carta(naipeTrunfo, pontosTrunfo, trunfoID, rankTrunfo);
            partida.trunfo = cartaTrunfo;
            partida.numVazas = numVazas;
            this.partidas = new List<Partida>();
            this.partidas.Add(partida);

            List<Vaza> listaVazas = new List<Vaza>();
            
            var xmlVazas = from al in doc.Element("GameModel").Element("Partida").Element("Vazas").Descendants("Vaza") select al;
            foreach (var newVaza in xmlVazas)
            {
                List<Jogada> listaJogadas = new List<Jogada>();
                var xmlCartasVaza = from al in newVaza.Descendants("Carta") select al;
                foreach (var newCarta in xmlCartasVaza)
                {
                    Jogada jogada = null;
                    int id = Convert.ToInt32(newCarta.Attribute("id").Value);
                    Carta carta;
                    if (id != -1)
	                {
		                NAIPE naipe = (NAIPE)(id/10);
                        int rank = id % 10;
                        int pontos = pontosFull[id-10*(id/10)];
                        carta = new Carta(naipe, pontos, id, rank);
	                }
                    else
                    {
                        //carta = new Carta(NAIPE.nenhum, -1, -1, -1);
                        carta = null;
                    }
                    int jID = Convert.ToInt32(newCarta.Attribute("jogadorId").Value);
                    IPlayer vazaPlayer = getPlayerFromID(jID);
                    if (vazaPlayer != null)
                    {
                        if (vazaPlayer is HumanPlayer)
                        {
                            jogada = new Jogada(vazaPlayer as HumanPlayer, carta);
                        }
                        else
                        {
                            jogada = new Jogada(vazaPlayer as BotPlayer, carta);
                        }
                    }
                    listaJogadas.Add(jogada);
                }
                listaVazas.Add(new Vaza(listaJogadas));
            }
            this.partidas[0].vazas = listaVazas;
            if (AnswerGameStart != null)
            {
                AnswerGameStart();
            }
        }

        private IPlayer getPlayerFromID(int id)
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.equipas[i%2].jogadores[i/2].ID == id)
                {
                    return this.equipas[i % 2].jogadores[i / 2];
                }
            }
            return null;
        }
    }
}
