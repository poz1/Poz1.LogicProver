using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    class R10 : IInferenceRule
    {
        private readonly ITermService termNamer;
        public R10(ITermService termNamer)
        {
            this.termNamer = termNamer;
        }

        public Sequent Apply(Sequent sequent)
        {
            QuantifierFormula implicationFormula = (QuantifierFormula)sequent.LeftHandSide.Formulas.Where
                (
                    x => x is QuantifierFormula formula && formula.Quantifier == QuantifierConnective.ForAll
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            var formula = implicationFormula.Formula.Clone();

            formula.ApplySubstitution(new Substitution( implicationFormula.Variable, LogicSolver.TermNamer.GetNewVariable()));

            sequent.LeftHandSide.Formulas.Remove(implicationFormula);
            sequent.LeftHandSide.Formulas.Add(formula);
            sequent.Justification = "R10 (" + sequent.Name + ")";

            return sequent;
        }
    }
}