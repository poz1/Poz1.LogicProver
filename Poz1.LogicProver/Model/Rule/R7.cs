﻿using Poz1.LogicProver.Model.Core;
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


            var formula = implicationFormula.Formula.Clone();

            if (formula.WorldIndex.IsGround && formula.FreeVariables.Count == 0)
                formula.WorldIndex.Symbols.Add(new WorldSymbol("1"));
            else
                formula.WorldIndex.Symbols.Add(new WorldSymbol("skolem function with blabla"));

            sequent.RightHandSide.Formulas.Remove(implicationFormula);
            sequent.RightHandSide.Formulas.Add(formula);

            sequent.Justification = "R7 (" + sequent.Name + ")";

            return sequent;

        }
    }
}

    
