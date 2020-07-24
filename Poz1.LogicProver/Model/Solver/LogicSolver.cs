using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.Rule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poz1.LogicProver.Model.Solver
{
    public class LogicSolver
    {
        public bool LogEnabled { get; set; }

        public IWorldNamer WorldNamer { get; set; }
        public ITermNamer TermNamer { get; set; }

        private ConstantWorldSymbol baseWorld;

        private readonly IResolutionRule resolutionRule;
        private readonly List<IInferenceRule> rules = new List<IInferenceRule>();

        private readonly List<string> Log = new List<string>();

        readonly AccessibilityRelation relation;

        public LogicSolver(AccessibilityRelation relation) {

            this.relation = relation;

            WorldNamer = new SimpleWorldNamer();
            TermNamer = new SimpleTermNamer();
            baseWorld = new ConstantWorldSymbol(WorldNamer.GetNewWorldConstant());

            //Only used on reduced sequents
            resolutionRule = new R1(relation);

            // These are applied as long as sequents have unreduced formulas
            rules.AddRange(new List<IInferenceRule>() { new R2(), new R3(), new R4(), new R5(), new R6(), new R7(WorldNamer), new R8(WorldNamer), new R9(TermNamer), new R10() });
        }

        public void Solve(Formula formula)
        {
            var initialSequent = new Sequent(formula);

            var queue = new Queue<Sequent>() ;

            var sequentCounter = 0;
            initialSequent.Name = sequentCounter.ToString();
            sequentCounter++;

            queue.Enqueue(initialSequent);
            var sequentList = new List<Sequent>();

            while (queue.Count != 0) {

                var sequent = queue.Dequeue();

                Parallel.ForEach(rules, x =>
                {
                    var result = x.Apply(sequent.Clone());
                    if (result != null)
                    {
                        result.Name = sequentCounter.ToString();
                        sequentCounter++;
                        queue.Enqueue(result);

                        Log.Add(result.ToString());
                        Console.WriteLine(result);

                        if (result.IsReduced) 
                            sequentList.Add(result);
                    }
                });

                resolutionRule.Apply(sequentList[0], sequentList[1]);
            }
        }
    }
}
