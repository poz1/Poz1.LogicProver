using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    class R10 : IInferenceRule
    {
        private readonly ITermNamer termNamer;
        public R10(ITermNamer termNamer)
        {
            this.termNamer = termNamer;
        }

        public Sequent Apply(Sequent sequent)
        {
            QuantifierFormula implicationFormula = (QuantifierFormula)sequent.LeftHandSide.Formulas.Where
                (
                    x => x is QuantifierFormula formula && formula.Quantifier == QuantifierConnective.ForAll
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            var formula = implicationFormula.Formula.Clone();

            formula.ApplySubstitution(new Substitution(new VariableTerminal(termNamer.GetNewVariable()), implicationFormula.Variable));

            sequent.LeftHandSide.Formulas.Remove(implicationFormula);
            sequent.LeftHandSide.Formulas.Add(formula);
            sequent.Justification = "R10 (" + sequent.Name + ")";

            return sequent;
        }
    }
}