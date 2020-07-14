using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public enum UnaryConnective
    {
        Negation,
        Necessity,
        Possibility
    }

    public enum BinaryConnective
    {
        Conjunction,
        Disjunction,
        Implication,
        Equivalence
    }
}
