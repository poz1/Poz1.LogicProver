using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.Solver;
using System;
using System.Collections.Generic;

namespace Poz1.LogicProver.Test
{
    [TestClass]

    public class ProverTest
    {

        [TestMethod]
        public void Example1()
        {
            var baseWorld = LogicSolver.WorldService.GetNewWorldConstant();
            var baseIndex = new WorldIndex(baseWorld);

            var relation = new AccessibilityRelation(new List<IRelationProperty>() { 
                new SerialProperty(),
                new TransitiveProperty()
            });

            var variable = LogicSolver.TermNamer.GetNewVariable();
            var baseFormula = new AtomicFormula(variable, baseIndex);
            var formula = new BinaryFormula(
                new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex),
                new UnaryFormula(new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex), UnaryConnective.Necessity, baseIndex), 
                BinaryConnective.Implication, baseIndex);

            var solver = new LogicSolver(relation);

            Assert.IsTrue(solver.Solve(formula));
        }

        [TestMethod]
        public void Example2()
        {
            var baseWorld = LogicSolver.WorldService.GetNewWorldConstant();
            var baseIndex = new WorldIndex(baseWorld);

            var relation = new AccessibilityRelation(new List<IRelationProperty>() {
                new SerialProperty(),
                new TransitiveProperty()
            });

            //x
            var variable = LogicSolver.TermNamer.GetNewVariable();
        

            var baseFormula = new AtomicFormula(variable, baseIndex);

            var formula = new BinaryFormula(
                new UnaryFormula(new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex), UnaryConnective.Necessity, baseIndex),
                new UnaryFormula(new UnaryFormula(baseFormula, UnaryConnective.Possibility, baseIndex), UnaryConnective.Possibility, baseIndex),
                BinaryConnective.Implication, baseIndex);

            var solver = new LogicSolver(relation);

            Assert.IsTrue(solver.Solve(formula));
        }

        [TestMethod]
        public void Example3()
        {
            var baseWorld = LogicSolver.WorldService.GetNewWorldConstant();
            var baseIndex = new WorldIndex(baseWorld);

            var relation = new AccessibilityRelation(new List<IRelationProperty>() {
                new SerialProperty(),
                new TransitiveProperty()
            });

            //x
            var variable = LogicSolver.TermNamer.GetNewVariable();


            var baseFormula = new AtomicFormula(variable, baseIndex);

            var formula = new BinaryFormula(
                new UnaryFormula(new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex), UnaryConnective.Possibility, baseIndex),
                new UnaryFormula(new UnaryFormula(baseFormula, UnaryConnective.Possibility, baseIndex), UnaryConnective.Necessity, baseIndex),
                BinaryConnective.Implication, baseIndex);

            var solver = new LogicSolver(relation);

            Assert.IsTrue(solver.Solve(formula));
        }

        [TestMethod]
        public void Example4()
        {
            var baseWorld = LogicSolver.WorldService.GetNewWorldConstant();
            var baseIndex = new WorldIndex(baseWorld);

            var relation = new AccessibilityRelation(new List<IRelationProperty>() {
                new SerialProperty(),
                new TransitiveProperty()
            });

            //x
            var variable = LogicSolver.TermNamer.GetNewVariable();
            var func = LogicSolver.TermNamer.GetNewFunction(new List<Terminal>() { variable });
            var baseFormula = new AtomicFormula(func, baseIndex);

            var formula = new BinaryFormula(
                new QuantifierFormula(new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex), variable, QuantifierConnective.ForAll, baseIndex),
                new UnaryFormula(new QuantifierFormula(baseFormula, variable, QuantifierConnective.ForAll, baseIndex), UnaryConnective.Necessity, baseIndex),
                BinaryConnective.Implication, baseIndex);

            var solver = new LogicSolver(relation);

            Assert.IsTrue(solver.Solve(formula));
        }

        [TestMethod]
        public void Example5()
        {
            var baseWorld = LogicSolver.WorldService.GetNewWorldConstant();
            var baseIndex = new WorldIndex(baseWorld);

            var relation = new AccessibilityRelation(new List<IRelationProperty>() {
                new SerialProperty(),
                new TransitiveProperty()
            });

            //x
            var variable = LogicSolver.TermNamer.GetNewVariable();
            var func = LogicSolver.TermNamer.GetNewFunction(new List<Terminal>() { variable });
            var baseFormula = new AtomicFormula(func, baseIndex);

            var formula = new BinaryFormula(
                new UnaryFormula(new QuantifierFormula(baseFormula, variable, QuantifierConnective.ForAll, baseIndex), UnaryConnective.Necessity, baseIndex),
                new QuantifierFormula(new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex), variable, QuantifierConnective.ForAll, baseIndex),
                BinaryConnective.Implication, baseIndex);

            var solver = new LogicSolver(relation);

            Assert.IsTrue(solver.Solve(formula));
        }

        [TestMethod]
        public void Example8()
        {
            var baseWorld = LogicSolver.WorldService.GetNewWorldConstant();
            var baseIndex = new WorldIndex(baseWorld);

            var relation = new AccessibilityRelation( new List<IRelationProperty>() {  new SerialProperty() } );

            var variable = LogicSolver.TermNamer.GetNewVariable();
            var baseFormula = new AtomicFormula(LogicSolver.TermNamer.GetNewFunction(new List<Terminal>() { variable }), baseIndex);
            var formula = new BinaryFormula(
                new QuantifierFormula(
                    new UnaryFormula(baseFormula, UnaryConnective.Necessity, baseIndex), variable, QuantifierConnective.ForAll, baseIndex),
                    new UnaryFormula(new QuantifierFormula(baseFormula, variable, QuantifierConnective.ForAll, baseIndex), UnaryConnective.Necessity, baseIndex),
                BinaryConnective.Implication, baseIndex);

            var solver = new LogicSolver(relation);
            
            Assert.IsTrue(solver.Solve(formula));
        }
    }
}
