using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public interface ITermService
    {
        public VariableTerminal GetNewVariable();
        public ConstantTerminal GetNewConstant();
        public FunctionTerminal GetNewFunction();
        public FunctionTerminal GetNewFunction(List<Terminal> parameters);
    }
}
