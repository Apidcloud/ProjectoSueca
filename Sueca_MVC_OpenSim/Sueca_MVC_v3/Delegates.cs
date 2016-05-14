using Sueca_MVC_v3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sueca_MVC_v3
{
    public delegate void StringEventHandler(string text);
    public delegate void IPlayerEventHandler(IPlayer player);
    public delegate void EmptyEventHandler();
    public delegate void ObjectEventHandler(object o);
    public delegate void IntEventHandler(int i);
    public delegate void IntIntEventHandler(int a, int b);

    public delegate void EmptyDelegate();
    public delegate void StringDelegate(string texto);
    public delegate void CartaDelegate(int posicaoCarta);
    public delegate void JogadorDelegate(IPlayer jogador);
}
