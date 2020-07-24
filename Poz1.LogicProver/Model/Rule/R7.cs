using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;
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
            else
            {
                var skolemVariables = new List<WorldSymbol>(formula.WorldIndex.Symbols);
                skolemVariables.AddRange(formula.FreeVariables.Select(x => x.ToWorldSymbol()));
                formula.WorldIndex.Symbols.Add(new FunctionWorldSymbol(worldNamer.GetNewWorldFunction(), skolemVariables));
            }
            sequent.RightHandSide.Formulas.Remove(implicationFormula);
            sequent.RightHandSide.Formulas.Add(formula);

            sequent.Justification = "R7 (" + sequent.Name + ")";

            return sequent;

        }
    }
}

    
