using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    class R10 : IInferenceRule
    {
        public List<Sequent> Apply(IList<Sequent> sequents)
        {
            var sequent = sequents[0];

            QuantifierFormula implicationFormula = (QuantifierFormula)sequent.LeftHandSide.UnreducedFormulas.Where(
                x => x is QuantifierFormula formula && formula.Quantifier == Quantifier.ForAll
                ).FirstOrDefault();

            if (implicationFormula != null)
            {
                sequent.LeftHandSide.UnreducedFormulas.Remove(implicationFormula);

                var result = new Sequent();

                implicationFormula.Formula.ApplySubstitution(new Substitution<Terminal>(
                    new List<MGU.Equation<Terminal>>() { new MGU.Equation<Terminal>(new VariableTerminal(), implicationFormula.Variable) }));

                result.LeftHandSide.UnreducedFormulas.AddRange(sequent.LeftHandSide.UnreducedFormulas);
                result.LeftHandSide.UnreducedFormulas.Add(implicationFormula);

                result.RightHandSide.UnreducedFormulas.AddRange(sequent.RightHandSide.UnreducedFormulas);


                return new List<Sequent>() { result };
            }

            return null;
        }
    }
}