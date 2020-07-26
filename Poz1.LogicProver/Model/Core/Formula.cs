using Poz1.LogicProver.Model.MGU;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Formula 
    {    
        public WorldIndex WorldIndex { get; set; }

        public abstract void ChangeWorldIndex(WorldIndex value);

        public abstract List<VariableTerminal> FreeVariables { get; }
        public abstract List<Terminal> Variables { get; }

        public Substitution Unify(Formula formula)
        {
            var mgu = new MostGeneralUnifier();

            var itemsCount = Variables.Count < formula.Variables.Count ? Variables.Count : formula.Variables.Count;

            for (int i = 0; i < itemsCount; i++)
            {
                mgu.AddEquation(Variables[i], formula.Variables[i]);
            }

            return mgu.Compute();
        }

        public abstract Formula Clone();
       

        public Substitution MUnify(AccessibilityRelation relation, Formula formula)
        {
            var unif = Unify(formula);
            var eta = relation.WorldUnify(WorldIndex, formula.WorldIndex);

            unif.Compose(eta);
            return unif;
        }

        public abstract void ApplySubstitution(Substitution substitution);

        public Formula(WorldIndex index)
        {
            WorldIndex = index;
        }

        public Formula()
        {
        }

        internal abstract Formula Simplify();

        public string ToWorldString()
        {
            return "|" + ToString() + "|" + WorldIndex.ToString();
        }
    }
}
