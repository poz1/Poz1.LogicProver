using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model
{
    public class LogicProver
    {
        public IList<InferenceRule> InferenceRules { get; set; }
        public bool IsVerbose { get; set; }

        //ResolutionRule resolutionRule
        //R* relation
        //worldsKeeper* worldskeeper
    }
}
