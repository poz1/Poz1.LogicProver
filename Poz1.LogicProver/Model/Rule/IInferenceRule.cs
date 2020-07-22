using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public interface IInferenceRule
    {
        public Sequent Apply(Sequent inputSequent);
    }
}
