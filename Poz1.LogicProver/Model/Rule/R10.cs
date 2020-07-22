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

            if (implicationFormula != null)
            {
                sequent.LeftHandSide.Formulas.Remove(implicationFormula);

                var result = new Sequent();

                implicationFormula.Formula.ApplySubstitution(new Substitution<Terminal>(
                    new List<MGU.Equation<Terminal>>() { new MGU.Equation<Terminal>(new VariableTerminal("T"), implicationFormula.Variable) }));

                result.LeftHandSide.Formulas.AddRange(sequent.LeftHandSide.Formulas);
                result.LeftHandSide.Formulas.Add(implicationFormula);

                result.RightHandSide.Formulas.AddRange(sequent.RightHandSide.Formulas);


                return  result ;
            }

            return null;
        }
    }
}