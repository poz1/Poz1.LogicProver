using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    // R1: If S,|p|i <- T and S' <- |q|j, T' and |p|i and |q|j m-unify with unification σ then Sσ U S'σ <- Tσ U T'σ
    public class R1 : IInferenceRule
    {
        public IList<Sequent> Apply(IList<Sequent> sequents)
        {
            if (sequents.Count != 2)
                throw new Exception();


        }
    }
}
