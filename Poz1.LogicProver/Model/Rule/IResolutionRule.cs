using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Rule
{
    public interface IResolutionRule
    {
        public List<Sequent> Apply(Sequent sequentX, Sequent sequentY);
    }
}