using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public interface ITermNamer
    {
        public string GetNewVariable();
        public string GetNewConstant();
        public string GetNewFunction();
    }
}
