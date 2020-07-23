using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class SimpleWorldNamer : IWorldNamer
    {
        public List<String> WorldConstantsNames = new List<string>() { "0", "1", "2", "3", "4", "5", "6" };
        public List<String> WorldVariablesNames = new List<string>() { "v", "w", "x", "y", "z", "p", "q", "r" };
        public List<String> WorldFunctionsNames = new List<string>() { "f", "g", "h", "i", "s", "t", "l", "m", "n", "o" };

        private int WorldVariablesCount = 0;
        private int WorldConstantsCount = 0;
        private int WorldFunctionsCount = 0;

        public string GetNewWorldConstant()
        {
            WorldConstantsCount++;
            if (WorldConstantsCount < WorldConstantsNames.Count)
                return WorldConstantsNames[WorldConstantsCount - 1];
            
            else
            {
                return WorldConstantsNames[(WorldConstantsCount - 1) % WorldConstantsNames.Count] + (WorldConstantsCount - 1) / WorldConstantsNames.Count;
            }
        }

        public string GetNewWorldFunction()
        {
            WorldFunctionsCount++;
            if (WorldFunctionsCount < WorldFunctionsNames.Count)
                return WorldFunctionsNames[WorldFunctionsCount - 1];

            else
            {
                return WorldFunctionsNames[(WorldFunctionsCount - 1) % WorldFunctionsNames.Count] + (WorldFunctionsCount - 1) / WorldFunctionsNames.Count;
            }
        }

        public string GetNewWorldVariable()
        {
            WorldVariablesCount++;
            if (WorldVariablesCount < WorldVariablesNames.Count)
                return WorldVariablesNames[WorldVariablesCount - 1];

            else
            {
                return WorldVariablesNames[(WorldVariablesCount - 1) % WorldVariablesNames.Count] + (WorldVariablesCount - 1) / WorldVariablesNames.Count;
            }
        }
    }
}
