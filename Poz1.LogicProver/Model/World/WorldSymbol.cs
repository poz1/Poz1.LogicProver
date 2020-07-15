namespace Poz1.LogicProver.Model.World
{
    public class WorldSymbol
    {
        public string Symbol { get; } 

        public WorldSymbolType Type { get; }

        public WorldSymbol ParentSymbol { get; }

        public bool IsGround => Type == WorldSymbolType.Numeral;

        public WorldSymbol(string name, WorldSymbol parent) : this(name)
        {
            ParentSymbol = parent;
        }

        public WorldSymbol(string name)
        {
            Symbol = name;
            Type = int.TryParse(name, out _) ? WorldSymbolType.Numeral : WorldSymbolType.WorldVariable;
        }
    }

    public enum WorldSymbolType
    {
        Numeral, 
        WorldVariable
    }
}