using Poz1.LogicProver.Model.MGU;
using Poz1.LogicProver.Model.World;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Formula 
    {    
        public WorldIndex WorldIndex { get; set; }

        public abstract void ChangeWorldIndex(WorldIndex value);

        public abstract List<VariableTerminal> FreeVariables { get; }

        public Substitution<Terminal> Unify(Formula formula)
        {
            var mgu = new MostGeneralUnifier();

            var itemsCount = FreeVariables.Count < formula.FreeVariables.Count ? FreeVariables.Count : formula.FreeVariables.Count;

            for (int i = 0; i < itemsCount; i++)
            {
                mgu.AddEquation(FreeVariables[i], formula.FreeVariables[i]);
            }

            return mgu.Compute();
        }

        public abstract Formula Clone();
       

        public Substitution<Terminal> MUnify(AccessibilityRelation relation, Formula formula)
        {
            var unif = Unify(formula);
            var eta = relation.WorldUnify(WorldIndex, formula.WorldIndex);

            unif.Compose(eta);
            return unif;
        }

        public void ApplySubstitution(Substitution<Terminal> substitution) { }

        public Formula(WorldIndex index)
        {
            WorldIndex = index;
        }

        public Formula()
        {
        }

        public string ToWorldString()
        {
            return "|" + ToString() + "|" + WorldIndex.ToString();
        }
    }
}
