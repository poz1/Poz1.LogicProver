using Poz1.LogicProver.Model;
using Poz1.LogicProver.Model.MGU;
using System;
using System.Collections.Generic;

namespace Poz1.LogicProver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var t1 = new FunctionTerminal()
            {
                Value = "Q",
                Parameters = new List<Terminal>()
                {
                    new ConstantTerminal() { Value = "a" },
                    new FunctionTerminal()
                    {
                        Value = "g",
                        Parameters = new List<Terminal>()
                        {
                            new VariableTerminal() { Value = "x" },
                            new ConstantTerminal() { Value = "a" }
                        }
                    },
                    new FunctionTerminal()
                    {
                        Value = "f",
                        Parameters = new List<Terminal>()
                        {
                            new VariableTerminal() { Value = "y" }
                        }
                    }
                }
            };

            var t2 = new FunctionTerminal()
            {
                Value = "Q",
                Parameters = new List<Terminal>()
                {
                    new ConstantTerminal() { Value = "a" },
                    new FunctionTerminal()
                    {
                        Value = "g",
                        Parameters = new List<Terminal>()
                        {
                            new FunctionTerminal()
                            {
                                Value = "f",
                                Parameters = new List<Terminal>()
                                {
                                    new ConstantTerminal() { Value = "b" }
                                }
                            },
                            new ConstantTerminal() { Value = "a" }
                        }
                    },
                    new VariableTerminal() { Value = "x" }
                }
            };

            var unifier = new MostGeneralUnifier();
            unifier.AddEquation(t1, t2);

            var lol = unifier.Compute();
        }
    }
}
