using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.World
{
    public class WorldIndex 
    {
        public bool IsGround => Symbols.All(x => x.IsGround);

        public List<WorldSymbol> Symbols = new List<WorldSymbol>();

        public WorldIndex ParentIndex { get; private set; }
        public WorldSymbol EndSymbol => Symbols.First();
        public WorldSymbol StartSymbol => Symbols.Last();

        public WorldIndex(WorldSymbol symbol)
        {
            Symbols.Add(symbol);

            if(symbol.ParentSymbol != null)
            {
                ParentIndex = new WorldIndex(symbol.ParentSymbol);
                Symbols.AddRange(ParentIndex.Symbols);
            }
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
            builder.Append(Symbols[0]);

            for(int i = 1; i < Symbols.Count; i++)
            {
                builder.Append(":");
                builder.Append(Symbols[i]);
            }

            return builder.ToString();
        }
    }
}
