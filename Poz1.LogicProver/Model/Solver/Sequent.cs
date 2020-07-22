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

        //public bool IsReduced => !LeftHandSide.HasUnreducedFormulas && !RightHandSide.HasUnreducedFormulas;
        public bool IsEmpty => LeftHandSide.Count == 0 && RightHandSide.Count == 0;

        public Sequent()
        {
            LeftHandSide = new SequentSide();
            RightHandSide = new SequentSide();
        }

        public Sequent(Formula formula) : this()
        {
            RightHandSide.Formulas.Add(formula);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(LeftHandSide.ToString());
            stringBuilder.Append(" ← ");
            stringBuilder.Append(RightHandSide.ToString());

            if (Justification != null)
            {
                stringBuilder.Append("  :: ");
                stringBuilder.Append(Justification);
            }

            return stringBuilder.ToString();
        }

        public Sequent Clone()
        {
            var clone = (Sequent)MemberwiseClone();
            clone.LeftHandSide = LeftHandSide.Clone();
            clone.RightHandSide = RightHandSide.Clone();

            return clone;
        }

        public class SequentSide
        {
            /// <summary>
            /// Non Atomic Formulas: Never try to unify these formulas!
            /// </summary>
            //public List<Formula> UnreducedFormulas = new List<Formula>();
            ///// <summary>
            ///// Atomic Formulas: Only try to unify these formulas!
            ///// </summary>
            //public List<Formula> ReducedFormulas = new List<Formula>();

            public List<Formula> Formulas = new List<Formula>();

            //public bool HasUnreducedFormulas => UnreducedFormulas.Count != 0;
            public int Count => Formulas.Count;

            public SequentSide Clone()
            {
                var clone =  (SequentSide)MemberwiseClone();
                clone.Formulas = new List<Formula>(Formulas);
                return clone;
            }

            public override string ToString()
            {
                var stringBuilder = new StringBuilder();

                if(Formulas.Count != 0)
                    for (int i = 0; i < Formulas.Count; i++)
                    {
                        stringBuilder.Append(Formulas[i]);

                        if (i != Formulas.Count - 1)
                            stringBuilder.Append("; ");
                    }
                else
                    stringBuilder.Append(" -- ");

                return stringBuilder.ToString();
            }
        }
    }
}


