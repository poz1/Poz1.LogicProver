using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class SimpleWorldNamer : IWorldService
    {
        public List<string> WorldConstantsNames = new List<string>() { "0", "1", "2", "3", "4", "5", "6" };
        public List<string> WorldVariablesNames = new List<string>() { "v", "w", "x", "y", "z", "p", "q", "r" };
        public List<string> WorldFunctionsNames = new List<string>() { "f", "g", "h", "i", "s", "t", "l", "m", "n", "o" };

        private int WorldVariablesCount = 0;
        private int WorldConstantsCount = 0;
        private int WorldFunctionsCount = 0;

        public ConstantWorldSymbol GetNewWorldConstant()
        {
            WorldConstantsCount++;
            if (WorldConstantsCount < WorldConstantsNames.Count)
                return new ConstantWorldSymbol(WorldConstantsNames[WorldConstantsCount - 1]);
            
            else
            {
                return new ConstantWorldSymbol(WorldConstantsNames[(WorldConstantsCount - 1) % WorldConstantsNames.Count] 
                    + (WorldConstantsCount - 1) / WorldConstantsNames.Count);
            }
        }

        public FunctionWorldSymbol GetNewWorldFunction()
        {
            WorldFunctionsCount++;
            if (WorldFunctionsCount < WorldFunctionsNames.Count)
                return new FunctionWorldSymbol( WorldFunctionsNames[WorldFunctionsCount - 1]);

            else
            {
                return new FunctionWorldSymbol(WorldFunctionsNames[(WorldFunctionsCount - 1) % WorldFunctionsNames.Count] 
                    + (WorldFunctionsCount - 1) / WorldFunctionsNames.Count);
            }
        }

        public VariableWorldSymbol GetNewWorldVariable()
        {
            WorldVariablesCount++;
            if (WorldVariablesCount < WorldVariablesNames.Count)
                return new VariableWorldSymbol(WorldVariablesNames[WorldVariablesCount - 1]);

            else
            {
                return new VariableWorldSymbol(WorldVariablesNames[(WorldVariablesCount - 1) % WorldVariablesNames.Count] 
                    + (WorldVariablesCount - 1) / WorldVariablesNames.Count);
            }
        }

        public FunctionWorldSymbol GetNewWorldFunction(List<WorldSymbol> parameters)
        {
            WorldFunctionsCount++;
            if (WorldFunctionsCount < WorldFunctionsNames.Count)
                return new FunctionWorldSymbol(WorldFunctionsNames[WorldFunctionsCount - 1], parameters);

            else
            {
                return new FunctionWorldSymbol(WorldFunctionsNames[(WorldFunctionsCount - 1) % WorldFunctionsNames.Count]
                    + (WorldFunctionsCount - 1) / WorldFunctionsNames.Count, parameters);
            }
        }
    }
}
