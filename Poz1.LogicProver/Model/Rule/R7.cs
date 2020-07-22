using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R7 : IInferenceRule
    {
        public Sequent Apply(Sequent sequent)
        {
            UnaryFormula implicationFormula = (UnaryFormula)sequent.RightHandSide.Formulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Necessity
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            sequent.RightHandSide.Formulas.Remove(implicationFormula);

            if (implicationFormula.WorldIndex.IsGround && implicationFormula.Formula.FreeVariables.Count == 0)
                implicationFormula.WorldIndex.Add(new WorldSymbol("nuovo"));
            else
                implicationFormula.WorldIndex.Add(new WorldSymbol("skolem function with blabla"));

            sequent.LeftHandSide.Formulas.Add(implicationFormula.Formula);


            return sequent;

        }
    }
}

    
