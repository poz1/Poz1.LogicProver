using Poz1.LogicProver.Model.Core;

namespace Poz1.LogicProver.Model.World
{
    public abstract class AccessibilityRelation
    {
        public Substitution WorldUnify(WorldIndex i, WorldIndex j)
        {
            //By convention "0" is the actual world
            if (i.StartSymbol.Symbol != "0" || j.StartSymbol.Symbol != "0")
                return null;

            if(i.EndSymbol.IsGround && j.EndSymbol.IsGround)
            {
                if (i.EndSymbol.Symbol == j.EndSymbol.Symbol)
                    return new Substitution(); 
                else
                    return null;
            }

            return AbstractWorldUnify(i, j);
        }

        public Substitution GetUnification(WorldSymbol i, WorldSymbol j)
        {
            var unification = new Substitution();

            return unification;
        }

        protected abstract Substitution AbstractWorldUnify(WorldIndex i, WorldIndex j);
    }
}
