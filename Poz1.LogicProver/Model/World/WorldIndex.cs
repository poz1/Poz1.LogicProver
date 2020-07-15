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
    }
}
