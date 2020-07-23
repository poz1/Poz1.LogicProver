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
        private readonly IWorldNamer worldNamer;
        public R8(IWorldNamer worldNamer)
        {
            this.worldNamer = worldNamer;
        }

        public Sequent Apply(Sequent sequent)
        {
            UnaryFormula implicationFormula = (UnaryFormula)sequent.LeftHandSide.Formulas.Where
                (
                    x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Necessity
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            sequent.LeftHandSide.Formulas.Remove(implicationFormula);

            var formula = implicationFormula.Formula.Clone();

            formula.WorldIndex.Symbols.Add(new WorldSymbol(worldNamer.GetNewWorldVariable()));
            sequent.LeftHandSide.Formulas.Add(formula);

            sequent.Justification = "R8 (" + sequent.Name + ")";

            return sequent;
         
        }
    }
}
