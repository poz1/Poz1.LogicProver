using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public interface IWorldService
    {
        public VariableWorldSymbol GetNewWorldVariable();
        public ConstantWorldSymbol GetNewWorldConstant();
        public FunctionWorldSymbol GetNewWorldFunction();

        public FunctionWorldSymbol GetNewWorldFunction(List<WorldSymbol> parameters);

    }
}
