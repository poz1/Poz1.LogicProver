using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R5 : IInferenceRule
    {
        public List<Sequent> Apply(IList<Sequent> sequents)
        {
            var sequent = sequents[0];

            UnaryFormula implicationFormula = (UnaryFormula)sequent.LeftHandSide.UnreducedFormulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Negation
                ).FirstOrDefault();

            if (implicationFormula != null)
            {
                sequent.LeftHandSide.UnreducedFormulas.Remove(implicationFormula);

                var result = new Sequent();

                result.LeftHandSide.UnreducedFormulas.AddRange(sequent.LeftHandSide.UnreducedFormulas);

                result.RightHandSide.UnreducedFormulas.AddRange(sequent.RightHandSide.UnreducedFormulas);
                result.RightHandSide.UnreducedFormulas.Add(implicationFormula.Formula);

                return new List<Sequent>() { result };
            }

            return null;
        }
    }
}
