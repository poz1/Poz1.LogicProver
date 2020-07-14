using Poz1.LogicProver.Model.Rule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model
{
    public class LogicProver
    {
        public IList<IInferenceRule> InferenceRules { get; set; }
        public bool IsVerbose { get; set; }
    }
}
