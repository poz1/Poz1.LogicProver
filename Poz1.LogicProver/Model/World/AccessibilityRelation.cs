using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace Poz1.LogicProver.Model.World
{
    public abstract class AccessibilityRelation
    {
        protected readonly Dictionary<WorldSymbol, List<WorldSymbol>> relations;

        public Substitution<WorldSymbol> WorldUnify(WorldIndex i, WorldIndex j)
        {
            //By convention "0" is the actual world
            if (i.StartSymbol.Symbol != "0" || j.StartSymbol.Symbol != "0")
                return null;

            if(i.EndSymbol.IsGround && j.EndSymbol.IsGround)
            {
                if (i.EndSymbol.Symbol == j.EndSymbol.Symbol)
                    return new Substitution<WorldSymbol> (); 
                else
                    return null;
            }

            return AbstractWorldUnify(i, j);
        }

        public bool Belongs(WorldSymbol x, WorldSymbol y)
        {
            return relations[x].Contains(y);
        }

        public abstract void Add(WorldSymbol x, WorldSymbol y);

        //public Substitution GetUnification(WorldSymbol i, WorldSymbol j)
        //{
        //    var unification = new Substitution();

        //    return unification;
        //}

        protected abstract Substitution<WorldSymbol> AbstractWorldUnify(WorldIndex i, WorldIndex j);
    }
}
