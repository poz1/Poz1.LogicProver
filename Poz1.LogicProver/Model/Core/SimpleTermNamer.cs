using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class SimpleTermNamer : ITermNamer
    {
        public List<String> TermConstantsNames = new List<string>() { "a", "b", "c", "d", "e" };
        public List<String> TermVariablesNames = new List<string>() { "x", "y", "v", "w", "z", "p", "q", "r" };
        public List<String> TermFunctionsNames = new List<string>() { "f", "g", "h", "i", "s", "t", "l", "m", "n", "o" };

        private int TermVariablesCount = 0;
        private int TermConstantsCount = 0;
        private int TermFunctionsCount = 0;

        public string GetNewConstant()
        {
            TermConstantsCount++;
            if (TermConstantsCount < TermConstantsNames.Count)
                return TermConstantsNames[TermConstantsCount - 1];

            else
            {
                return TermConstantsNames[(TermConstantsCount - 1) % TermConstantsNames.Count] + (TermConstantsCount - 1) / TermConstantsNames.Count;
            }
        }

        public string GetNewFunction()
        {
            TermFunctionsCount++;
            if (TermFunctionsCount < TermFunctionsNames.Count)
                return TermFunctionsNames[TermFunctionsCount - 1];

            else
            {
                return TermFunctionsNames[(TermFunctionsCount - 1) % TermFunctionsNames.Count] + (TermFunctionsCount - 1) / TermFunctionsNames.Count;
            }
        }

        public string GetNewVariable()
        {
            TermVariablesCount++;
            if (TermVariablesCount < TermVariablesNames.Count)
                return TermVariablesNames[TermVariablesCount - 1];

            else
            {
                return TermVariablesNames[(TermVariablesCount - 1) % TermVariablesNames.Count] + (TermVariablesCount - 1) / TermVariablesNames.Count;
            }
        }
    }
}
