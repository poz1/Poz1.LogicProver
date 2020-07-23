using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.World
{
    public interface IWorldNamer
    {
        public string GetNewWorldVariable();
        public string GetNewWorldConstant();
        public string GetNewWorldFunction();
    }
}
