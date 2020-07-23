using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver.Model.Rule
{
    public class R9 : IInferenceRule
    {
        private readonly ITermNamer termNamer;
        public R9(ITermNamer termNamer)
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
                a = new ConstantTerminal(termNamer.GetNewConstant());
            else
            {
                var skolemVariables = new List<ILogicElement>(formula.WorldIndex.Symbols);
                skolemVariables.AddRange(formula.FreeVariables);
                a = new FunctionTerminal(termNamer.GetNewFunction(), skolemVariables); 
            }

            formula.ApplySubstitution(new Substitution(a, implicationFormula.Variable));

            sequent.RightHandSide.Formulas.Remove(implicationFormula);
            sequent.RightHandSide.Formulas.Add(formula);

            sequent.Justification = "R9 (" + sequent.Name + ")";
            return sequent;

        }
    }
}
