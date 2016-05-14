using Phone.Classes.Models;

namespace Phone.Classes
{
    public delegate void StringEventHandler(string text);
    public delegate void IPlayerEventHandler(IPlayer player);
    public delegate void EmptyEventHandler();
    public delegate void ObjectEventHandler(object o);
    public delegate void IntEventHandler(int i);
    public delegate void IntIntEventHandler(int a, int b);
}
