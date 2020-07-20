using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    class R6 : IInferenceRule
    {
        public List<Sequent> Apply(IList<Sequent> sequents)
        {
            var sequent = sequents[0];

            UnaryFormula implicationFormula = (UnaryFormula)sequent.RightHandSide.UnreducedFormulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Negation
                ).FirstOrDefault();

            if (implicationFormula != null)
            {
                sequent.LeftHandSide.UnreducedFormulas.Remove(implicationFormula);

                var result = new Sequent();

                result.LeftHandSide.UnreducedFormulas.AddRange(sequent.LeftHandSide.UnreducedFormulas);
                result.LeftHandSide.UnreducedFormulas.Add(implicationFormula.Formula);

                result.RightHandSide.UnreducedFormulas.AddRange(sequent.RightHandSide.UnreducedFormulas);

                return new List<Sequent>() { result };
            }

            return null;
        }
    }
}
