using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R2 : IInferenceRule
    {
        // R2: If S, |(p->q)|i <- T then S, |q|i <-|p|i, T
        public IList<Sequent> Apply(IList<Sequent> sequents)
        {
            throw new NotImplementedException();
        }
    }
}
