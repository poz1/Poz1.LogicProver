namespace Poz1.LogicProver.Model.World
{
    public class WorldSymbol
    {
        public string Symbol { get; set; }

        public WorldSymbolType Type { get; set; }

        public WorldSymbol ParentSymbol { get; set; }

        public bool IsGround => Type == WorldSymbolType.Numeral;
    }

    public enum WorldSymbolType
    {
        Numeral, 
        WorldVariable
    }
}