using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.World
{
    public class WorldIndex : List<WorldSymbol>
    {
        public bool IsGround => this.All(x => x.IsGround);

        public WorldIndex ParentIndex { get; }
        public WorldSymbol EndSymbol => this.First();
        public WorldSymbol StartSymbol => this.Last();

        public WorldIndex(WorldSymbol symbol)
        {
            Add(symbol);

            if(symbol.ParentSymbol != null)
            {
                ParentIndex = new WorldIndex(symbol.ParentSymbol);
                AddRange(ParentIndex);
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(this[0]);

            for(int i = 1; i < Count; i++)
            {
                builder.Append(":");
                builder.Append(this[i]);
            }

            return builder.ToString();
        }
    }
}
