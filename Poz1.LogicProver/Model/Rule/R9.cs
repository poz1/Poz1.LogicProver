using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver.Model.Rule
{
    public class R9 : IInferenceRule
    {
        private readonly ITermService termNamer;
        public R9(ITermService termNamer)
        {
            this.termNamer = termNamer;
        }

        public Sequent Apply(Sequent sequent)
        {
            QuantifierFormula implicationFormula = (QuantifierFormula)sequent.RightHandSide.Formulas.Where
                (
                    x => x is QuantifierFormula formula && formula.Quantifier == QuantifierConnective.ForAll
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            var formula = implicationFormula.Formula.Clone();

            Terminal a;

            if (implicationFormula.Formula.FreeVariables.Count == 0 && implicationFormula.WorldIndex.IsGround)
                a = termNamer.GetNewConstant();
            else
            {
                var skolemVariables = new List<Terminal>(formula.WorldIndex.Symbols.Select(x => x.ToTerminal()));
                skolemVariables.AddRange(formula.FreeVariables);
                a = termNamer.GetNewFunction(skolemVariables); 
            }

            formula.ApplySubstitution(new Substitution(a, implicationFormula.Variable));

            sequent.RightHandSide.Formulas.Remove(implicationFormula);
            sequent.RightHandSide.Formulas.Add(formula);

            sequent.Justification = "R9 (" + sequent.Name + ")";
            return sequent;

        }
    }
}
