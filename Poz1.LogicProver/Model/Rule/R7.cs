using Poz1.LogicProver.Model.Core;
using System.Linq;

namespace Poz1.LogicProver.Model.Rule
{
    public class R7 : IInferenceRule
    {
        private readonly IWorldNamer worldNamer;
        public R7(IWorldNamer worldNamer)
        {
            this.worldNamer = worldNamer;
        }

        public Sequent Apply(Sequent sequent)
        {
            UnaryFormula implicationFormula = (UnaryFormula)sequent.RightHandSide.Formulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Necessity
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;


            var formula = implicationFormula.Formula.Clone();

            if (formula.WorldIndex.IsGround && formula.FreeVariables.Count == 0)
                formula.WorldIndex.AddSymbol(new ConstantWorldSymbol(worldNamer.GetNewWorldConstant()));
            //else
            //    formula.WorldIndex.Symbols.Add(new FunctionWorldSymbol("skolem function with blabla"));

            sequent.RightHandSide.Formulas.Remove(implicationFormula);
            sequent.RightHandSide.Formulas.Add(formula);

            sequent.Justification = "R7 (" + sequent.Name + ")";

            return sequent;

        }
    }
}

    
