using Poz1.LogicProver.Model;
using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.MGU;
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

            var baseWorld = new WorldSymbol("0");
            var wi1 = new WorldIndex(new WorldSymbol("f(w)", new WorldSymbol("w", baseWorld)));
            var wi2 = new WorldIndex(new WorldSymbol("w1", baseWorld));

            var relation = new AccessibilityRelation(
              new List<IRelationProperty>()
              {
                    new SerialProperty()
              },
              new List<WorldIndex>() { wi1, wi2 }
            );

            var t = relation.WorldUnify(wi1, wi2);
        }
    }
}
