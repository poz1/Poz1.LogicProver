using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = new UnicodeEncoding();
            Console.WriteLine("Hello World!");

            #region test8

            var baseWorld = new ConstantWorldSymbol("0");
            var baseIndex = new WorldIndex(baseWorld);

            var relation = new AccessibilityRelation(new List<IRelationProperty>()  {
                    new SerialProperty()
            });


            var variable = LogicSolver.TermNamer.GetNewVariable();
            var atomicFormula = new AtomicFormula(LogicSolver.TermNamer.GetNewFunction(new List<Terminal>() { variable }), baseIndex);
            var formula = new BinaryFormula(
                new QuantifierFormula(new UnaryFormula(atomicFormula, UnaryConnective.Necessity, baseIndex), variable, QuantifierConnective.ForAll, baseIndex),
                new UnaryFormula(new QuantifierFormula(atomicFormula, variable, QuantifierConnective.ForAll, baseIndex), UnaryConnective.Necessity, baseIndex),
                BinaryConnective.Implication, baseIndex);

            #endregion

            #region test1

            //var baseWorld = LogicSolver.WorldService.GetNewWorldConstant();
            //var baseIndex = new WorldIndex(baseWorld);

            //var relation = new AccessibilityRelation(new List<IRelationProperty>() {
            //    new SerialProperty(),
            //    new TransitiveProperty()
            //});

            //var variable = LogicSolver.TermNamer.GetNewVariable();
            //var baseFormula = new AtomicFormula(variable, baseIndex);
            //var formula = new BinaryFormula(
            //    new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex),
            //    new UnaryFormula(new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex), UnaryConnective.Necessity, baseIndex),
            //    BinaryConnective.Implication, baseIndex);

            #endregion

            Console.WriteLine();
            Console.WriteLine("Formula: " + formula.ToWorldString());
            var solver = new LogicSolver(relation);
            var result = solver.Solve(formula);

            Console.WriteLine("Proof found: " + result);

        }
    }
}
