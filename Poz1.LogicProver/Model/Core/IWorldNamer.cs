using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public interface IWorldNamer
    {
        public string GetNewWorldVariable();
        public string GetNewWorldConstant();
        public string GetNewWorldFunction();
    }
}
