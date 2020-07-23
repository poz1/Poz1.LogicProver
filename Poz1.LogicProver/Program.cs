using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.Solver;
using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;

namespace Poz1.LogicProver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var t1 = new FunctionTerminal()
            //{
            //    Value = "Q",
            //    Parameters = new List<Terminal>()
            //    {
            //        new ConstantTerminal() { Value = "a" },
            //        new FunctionTerminal()
            //        {
            //            Value = "g",
            //            Parameters = new List<Terminal>()
            //            {
            //                new VariableTerminal() { Value = "x" },
            //                new ConstantTerminal() { Value = "a" }
            //            }
            //        },
            //        new FunctionTerminal()
            //        {
            //            Value = "f",
            //            Parameters = new List<Terminal>()
            //            {
            //                new VariableTerminal() { Value = "y" }
            //            }
            //        }
            //    }
            //};

            //var t2 = new FunctionTerminal()
            //{
            //    Value = "Q",
            //    Parameters = new List<Terminal>()
            //    {
            //        new ConstantTerminal() { Value = "a" },
            //        new FunctionTerminal()
            //        {
            //            Value = "g",
            //            Parameters = new List<Terminal>()
            //            {
            //                new FunctionTerminal()
            //                {
            //                    Value = "f",
            //                    Parameters = new List<Terminal>()
            //                    {
            //                        new ConstantTerminal() { Value = "b" }
            //                    }
            //                },
            //                new ConstantTerminal() { Value = "a" }
            //            }
            //        },
            //        new VariableTerminal() { Value = "x" }
            //    }
            //};

            //var unifier = new MostGeneralUnifier();
            //unifier.AddEquation(t1, t2);

            //var lol = unifier.Compute();

            var baseWorld = new ConstantWorldSymbol("0");
            //var wi1 = new WorldIndex(new WorldSymbol("f(w)", new WorldSymbol("w", baseWorld)));
            var baseIndex = new WorldIndex(baseWorld);

            //var relation = new AccessibilityRelation(
            //  new List<IRelationProperty>()
            //  {
            //        new SerialProperty()
            //  },
            //  new List<WorldIndex>() { wi1, wi2 }
            //);

            //var t = relation.WorldUnify(wi1, wi2);
            var atomicFormula = new AtomicFormula(new ConstantTerminal("p"), baseIndex);
            var formulaEX1 = new BinaryFormula(
                new UnaryFormula(atomicFormula, UnaryConnective.Necessity, baseIndex),
                new UnaryFormula(
                    new UnaryFormula(atomicFormula, UnaryConnective.Necessity, baseIndex), UnaryConnective.Necessity, baseIndex),
                BinaryConnective.Implication, baseIndex);


            var varia = new VariableTerminal("x");
            var atomicFormula8 = new AtomicFormula(new FunctionTerminal("p", new List<Terminal>() { varia }), baseIndex);
            var formulaEX8 = new BinaryFormula(
             new QuantifierFormula( new UnaryFormula(atomicFormula8, UnaryConnective.Necessity, baseIndex), varia, QuantifierConnective.ForAll, baseIndex),
              new UnaryFormula(new QuantifierFormula(atomicFormula8, varia,  QuantifierConnective.ForAll, baseIndex), UnaryConnective.Necessity, baseIndex),
              BinaryConnective.Implication, baseIndex);

            Console.WriteLine(formulaEX8.ToWorldString());
            var solver = new LogicSolver();
            solver.Solve(formulaEX8);
        }
    }
}
