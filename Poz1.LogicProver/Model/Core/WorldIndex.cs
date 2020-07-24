using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class WorldIndex 
    {
        public bool IsGround => Symbols.All(x => x.IsGround);

        public List<WorldSymbol> Symbols = new List<WorldSymbol>();

        public WorldIndex ParentIndex { get; private set; }
        public WorldSymbol EndSymbol => Symbols.Last();
        public WorldSymbol StartSymbol => Symbols.First();

        public WorldIndex(WorldSymbol symbol)
        {
            Symbols.Add(symbol);

            if(symbol.ParentSymbol != null)
            {
                ParentIndex = new WorldIndex(symbol.ParentSymbol);
                Symbols.AddRange(ParentIndex.Symbols);
            }
        }

        public void AddSymbol(WorldSymbol symbol)
        {
            symbol.ParentSymbol = EndSymbol;
            Symbols.Add(symbol);
            ParentIndex = new WorldIndex(symbol.ParentSymbol);
        }

        public WorldIndex Clone()
        {
            var clone = (WorldIndex)MemberwiseClone();
            clone.ParentIndex = ParentIndex?.Clone();
            clone.Symbols = new List<WorldSymbol>(Symbols);
            return clone;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(EndSymbol);

            for(int i = Symbols.Count - 2; i >= 0; i--)
            {
                builder.Append(":");
                builder.Append(Symbols[i]);
            }

            return builder.ToString();
        }
    }
}
