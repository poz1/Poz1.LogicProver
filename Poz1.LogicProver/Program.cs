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

            //TEST WUnificaton 1
            //var baseWorld = new WorldSymbol("0");
            //var num = new WorldIndex(new WorldSymbol("2", new WorldSymbol("1", baseWorld)));
            //var var = new WorldIndex(new WorldSymbol("w",  baseWorld));

            //var serialRelation = new SerialRelation(new List<WorldIndex>(){ num, var });
            //var lol = serialRelation.WorldUnify(num, var);

            //var reflexiveTransRel = new ReflexiveTransitiveSerialRelation(new List<WorldIndex>() { num, var });
            //var lol2 = reflexiveTransRel.WorldUnify(num, var);

            //TEST WUnificaton 2
            //var baseWorld = new WorldSymbol("0");
            //var num = new WorldIndex(new WorldSymbol("w", new WorldSymbol("v", baseWorld)));
            //var var = new WorldIndex(new WorldSymbol("k", new WorldSymbol("u", baseWorld)));

            //var serialRelation = new SerialRelation(new List<WorldIndex>() { num, var });
            //var lol = serialRelation.WorldUnify(num, var);
            
            //TEST WUnificaton 3
            var baseWorld = new WorldSymbol("0");
            var num = new WorldIndex(new WorldSymbol("w", new WorldSymbol("1", baseWorld)));
            var var = new WorldIndex(new WorldSymbol("v", new WorldSymbol("2", baseWorld)));

            var rel = new AccessibilityRelation(
                new List<IRelationProperty>()
                {
                    new SimmetricProperty(),
                    new TransitiveProperty()
                },
                new List<WorldIndex>() { num, var }
            ); ;
            //var serialRelation = new TransitiveSimmetricRelation(new List<WorldIndex>() { num, var });
            //var lol = serialRelation.WorldUnify(num, var);
        }
    }
}
