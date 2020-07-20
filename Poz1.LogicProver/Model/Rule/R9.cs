using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R9 : IInferenceRule
    {
        public List<Sequent> Apply(IList<Sequent> sequents)
        {
            var sequent = sequents[0];

            QuantifierFormula implicationFormula = (QuantifierFormula)sequent.RightHandSide.UnreducedFormulas.Where(
                x => x is QuantifierFormula formula && formula.Quantifier == Quantifier.ForAll
                ).FirstOrDefault();

            if (implicationFormula != null)
            {
                sequent.RightHandSide.UnreducedFormulas.Remove(implicationFormula);

                var result = new Sequent();


                Terminal a;

                if (implicationFormula.Formula.FreeVariables.Count == 0 && implicationFormula.WorldIndex.IsGround)
                    a = new ConstantTerminal(); //TODO fix name
                else
                    a = new FunctionTerminal(); //Skolem di blalbal


                implicationFormula.Formula.ApplySubstitution(new Substitution<Terminal>(
                    new List<MGU.Equation<Terminal>>() {new MGU.Equation<Terminal>(a, implicationFormula.Variable) }));

                result.LeftHandSide.UnreducedFormulas.AddRange(sequent.LeftHandSide.UnreducedFormulas);

                result.RightHandSide.UnreducedFormulas.AddRange(sequent.RightHandSide.UnreducedFormulas);
                result.RightHandSide.UnreducedFormulas.Add(implicationFormula);
                  

                return new List<Sequent>() { result };
            }

            return null;
        }
    }
}
