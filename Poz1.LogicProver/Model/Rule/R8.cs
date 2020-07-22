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
        public Sequent Apply(Sequent sequent)
        {
            UnaryFormula implicationFormula = (UnaryFormula)sequent.LeftHandSide.Formulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Necessity
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            sequent.LeftHandSide.Formulas.Remove(implicationFormula);


                implicationFormula.WorldIndex.Add(new WorldSymbol("new world var"));

            sequent.LeftHandSide.Formulas.Add(implicationFormula.Formula);


                return sequent;
         
        }
    }
}
