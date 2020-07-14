using Poz1.LogicProver.Model.Core;
using System;

namespace Poz1.LogicProver.Model.World
{
    public class SerialRelation : AccessibilityRelation
    {
        protected override Substitution AbstractWorldUnify(WorldIndex i, WorldIndex j)
        {
            //2b
            if (i.EndSymbol.IsGround || j.EndSymbol.IsGround)
            {
                //Need to verify if n is accessible from the parent of w
                var n = i.EndSymbol.IsGround ? i.EndSymbol : j.EndSymbol;
                var w = !i.EndSymbol.IsGround ? i.EndSymbol : j.EndSymbol;

                return new Substitution() { { w.Symbol, n.Symbol } };
            }
            ////2c
            //else
            //{
            //    //Misses a lot of things
            //    return new Substitution() { { i.EndSymbol.Symbol, j.EndSymbol.Symbol } };
            //}
        }
    }
}
