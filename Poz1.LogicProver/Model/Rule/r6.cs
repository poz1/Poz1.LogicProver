using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    class R6 : IInferenceRule
    {
        public Sequent Apply(Sequent sequent)
        {
            UnaryFormula implicationFormula = (UnaryFormula)sequent.RightHandSide.Formulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Negation
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            sequent.RightHandSide.Formulas.Remove(implicationFormula);

            var formula = implicationFormula.Formula.Clone();

            sequent.LeftHandSide.Formulas.Add(formula);
            sequent.Justification = "R6 (" + sequent.Name + ")";

            return sequent;
        }
    }
}
