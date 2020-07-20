using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model
{
    public class Sequent
    {
        public string Name { get; set; }
        public SequentSide LeftHandSide { get; set; }
        public SequentSide RightHandSide { get; set; }
        public string Justification { get; set; }

        public bool IsReduced => !LeftHandSide.HasUnreducedFormulas && !RightHandSide.HasUnreducedFormulas;
        public bool IsEmpty => LeftHandSide.Count == 0 && RightHandSide.Count == 0;

        public Sequent()
        {
            LeftHandSide = new SequentSide();
            RightHandSide = new SequentSide();
        }
    }

    public class SequentSide
    {
        public List<Formula> UnreducedFormulas = new List<Formula>();
        public List<Formula> ReducedFormulas = new List<Formula>();
        public bool HasUnreducedFormulas => UnreducedFormulas.Count != 0;
        public int Count => UnreducedFormulas.Count + ReducedFormulas.Count;
    }
}


