using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R8 : IInferenceRule
    {
        public List<Sequent> Apply(IList<Sequent> sequents)
        {
            var sequent = sequents[0];

            UnaryFormula implicationFormula = (UnaryFormula)sequent.LeftHandSide.UnreducedFormulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Necessity
                ).FirstOrDefault();

            if (implicationFormula != null)
            {
                sequent.LeftHandSide.UnreducedFormulas.Remove(implicationFormula);

                var result = new Sequent();

                implicationFormula.WorldIndex.Add(new WorldSymbol("new world var"));

                result.LeftHandSide.UnreducedFormulas.AddRange(sequent.LeftHandSide.UnreducedFormulas);
                result.LeftHandSide.UnreducedFormulas.Add(implicationFormula.Formula);

                result.RightHandSide.UnreducedFormulas.AddRange(sequent.RightHandSide.UnreducedFormulas);

                return new List<Sequent>() { result };
            }

            return null;
        }
    }
}
