using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    class R10 : IInferenceRule
    {
        public Sequent Apply(Sequent sequent)
        {
            QuantifierFormula implicationFormula = (QuantifierFormula)sequent.LeftHandSide.Formulas.Where(
                x => x is QuantifierFormula formula && formula.Quantifier == QuantifierConnective.ForAll
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;


            sequent.LeftHandSide.Formulas.Remove(implicationFormula);

            implicationFormula.Formula.ApplySubstitution(new Substitution<Terminal>(
                new List<MGU.Equation<Terminal>>() { new MGU.Equation<Terminal>(new VariableTerminal("T"), implicationFormula.Variable) }));

            sequent.LeftHandSide.Formulas.Add(implicationFormula);
            sequent.Justification = "R10 (" + sequent.Name + ")";

            return sequent;
        }
    }
}