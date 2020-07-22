﻿using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R9 : IInferenceRule
    {
        public Sequent Apply(Sequent sequent)
        {
            QuantifierFormula implicationFormula = (QuantifierFormula)sequent.RightHandSide.Formulas.Where(
                x => x is QuantifierFormula formula && formula.Quantifier == QuantifierConnective.ForAll
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            sequent.RightHandSide.Formulas.Remove(implicationFormula);



                Terminal a;

                if (implicationFormula.Formula.FreeVariables.Count == 0 && implicationFormula.WorldIndex.IsGround)
                    a = new ConstantTerminal("T"); //TODO fix name
                else
                    a = new FunctionTerminal("T", new List<Terminal> ()); //Skolem di blalbal


                implicationFormula.Formula.ApplySubstitution(new Substitution<Terminal>(
                    new List<MGU.Equation<Terminal>>() {new MGU.Equation<Terminal>(a, implicationFormula.Variable) }));


            sequent.RightHandSide.Formulas.Add(implicationFormula);
                  

                return sequent;

        }
    }
}
