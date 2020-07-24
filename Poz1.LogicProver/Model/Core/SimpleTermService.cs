using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class SimpleTermService : ITermService
    {
        public List<String> TermConstantsNames = new List<string>() { "a", "b", "c", "d", "e" };
        public List<String> TermVariablesNames = new List<string>() { "x", "y", "v", "w", "z", "p", "q", "r" };
        public List<String> TermFunctionsNames = new List<string>() { "f", "g", "h", "i", "s", "t", "l", "m", "n", "o" };

        private int TermVariablesCount = 0;
        private int TermConstantsCount = 0;
        private int TermFunctionsCount = 0;

        public ConstantTerminal GetNewConstant()
        {
            TermConstantsCount++;
            if (TermConstantsCount < TermConstantsNames.Count)
                return new ConstantTerminal(TermConstantsNames[TermConstantsCount - 1]);

            else
            {
                return new ConstantTerminal(TermConstantsNames[(TermConstantsCount - 1) % TermConstantsNames.Count]
                    + (TermConstantsCount - 1) / TermConstantsNames.Count);
            }
        }

        public FunctionTerminal GetNewFunction()
        {
            TermFunctionsCount++;
            if (TermFunctionsCount < TermFunctionsNames.Count)
                return new FunctionTerminal(TermFunctionsNames[TermFunctionsCount - 1]);

            else
            {
                return new FunctionTerminal(TermFunctionsNames[(TermFunctionsCount - 1) % TermFunctionsNames.Count]
                    + (TermFunctionsCount - 1) / TermFunctionsNames.Count);
            }
        }

        public VariableTerminal GetNewVariable()
        {
            TermVariablesCount++;
            if (TermVariablesCount < TermVariablesNames.Count)
                return new VariableTerminal(TermVariablesNames[TermVariablesCount - 1]);

            else
            {
                return new VariableTerminal(TermVariablesNames[(TermVariablesCount - 1) % TermVariablesNames.Count]
                    + (TermVariablesCount - 1) / TermVariablesNames.Count);
            }
        }

        public FunctionTerminal GetNewFunction(List<Terminal> parameters)
        {
            TermFunctionsCount++;
            if (TermFunctionsCount < TermFunctionsNames.Count)
                return new FunctionTerminal(TermFunctionsNames[TermFunctionsCount - 1], parameters);

            else
            {
                return new FunctionTerminal(TermFunctionsNames[(TermFunctionsCount - 1) % TermFunctionsNames.Count]
                    + (TermFunctionsCount - 1) / TermFunctionsNames.Count, parameters);
            }
        }
    }
}
