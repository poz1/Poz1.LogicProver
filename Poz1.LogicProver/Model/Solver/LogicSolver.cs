using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.Rule;
using Poz1.LogicProver.Model.World;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poz1.LogicProver.Model.Solver
{
    public class LogicSolver
    {
        public bool LogEnabled { get; set; }

        private int VariablesCount = 0;
        private int ConstantsCount = 0;
        private int FunctionsCount = 0;

        private WorldSymbol baseWorld = new WorldSymbol("0");

        private IResolutionRule resolutionRule;
        private List<IInferenceRule> rules = new List<IInferenceRule>();

        private List<string> Log = new List<string>();

        public LogicSolver() {

            //Only used on reduced sequents
            resolutionRule = new R1();

            // These are applied as long as sequents have unreduced formulas
            rules.AddRange(new List<IInferenceRule>() { new R2(), new R3(), new R4(), new R5(), new R6(), new R7(), new R8(), new R9(), new R10() });
        }

        public void Solve(Formula formula)
        {
            var initialSequent = new Sequent(formula);

            var queue = new Queue<Sequent>() ;
            queue.Enqueue(initialSequent);

            while (queue.Count != 0) {

                var sequent = queue.Dequeue();

                Parallel.ForEach(rules, x =>
                {
                    var result = x.Apply(sequent);
                    if (result != null)
                        queue.Enqueue(result);
                });
            }
        }

        //public Task SolveSequent(Sequent sequent, IInferenceRule rule)
        //{
        //    var sequents = new List<Sequent> { initialSequent };


        //}
    }
}
