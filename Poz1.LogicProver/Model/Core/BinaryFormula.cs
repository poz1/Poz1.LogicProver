﻿using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class BinaryFormula : Formula
    {
        public string Connective { get; set; }

        private Formula lHSFormula;
        public Formula LHSFormula
        {
            get => lHSFormula; set
            {
                lHSFormula = value;
                lHSFormula.WorldIndex = WorldIndex;
            }
        }

        private Formula rHSFormula;
        public Formula RHSFormula
        {
            get => rHSFormula; set
            {
                rHSFormula = value;
                rHSFormula.WorldIndex = WorldIndex;
            }
        }

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        public override List<Terminal> Variables => ComputeVariables();

        public BinaryFormula(Formula leftFormula, Formula rightFormula, string connective, WorldIndex index) : base(index)
        {
            LHSFormula = leftFormula;
            RHSFormula = rightFormula;
            Connective = connective;
        }


        private List<Terminal> ComputeVariables()
        {
            var vars = new List<Terminal>();
            vars.AddRange(LHSFormula.Variables);
            vars.AddRange(RHSFormula.Variables);
            return vars;
        }

        private List<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            vars.AddRange(LHSFormula.FreeVariables);
            vars.AddRange(RHSFormula.FreeVariables);
            return vars;
        }

        public override void ApplySubstitution(Substitution substitution)
        {
            LHSFormula.ApplySubstitution(substitution);
            RHSFormula.ApplySubstitution(substitution);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(LHSFormula.ToString());
            stringBuilder.Append(Connective);
            stringBuilder.Append(RHSFormula.ToString());


            return stringBuilder.ToString();
        }

        public override void ChangeWorldIndex(WorldIndex value)
        {
            WorldIndex = value;
            LHSFormula.ChangeWorldIndex(value);
            RHSFormula.ChangeWorldIndex(value);
        }

        public override Formula Clone()
        {
            var clone = (BinaryFormula)MemberwiseClone();
            clone.LHSFormula = LHSFormula.Clone();
            clone.RHSFormula = RHSFormula.Clone();
            clone.WorldIndex = WorldIndex.Clone();
            clone.ChangeWorldIndex(clone.WorldIndex);
            return clone;
        }

        internal override Formula Simplify()
        {
            RHSFormula = RHSFormula.Simplify();
            LHSFormula = LHSFormula.Simplify();

            switch (Connective)
            {
                // A ∧ B = ~ ( A → ~ B )
                case BinaryConnective.Conjunction:
                    return new UnaryFormula(
                        new BinaryFormula(
                            LHSFormula,
                            new UnaryFormula(RHSFormula, UnaryConnective.Negation, RHSFormula.WorldIndex), 
                            BinaryConnective.Implication, WorldIndex),
                        UnaryConnective.Negation, WorldIndex);

                // A ∨ B = ~ A → B
                case BinaryConnective.Disjunction:
                    return new BinaryFormula(
                             new UnaryFormula(LHSFormula, UnaryConnective.Negation, LHSFormula.WorldIndex),
                             RHSFormula,
                            BinaryConnective.Implication, WorldIndex);

                // A ↔ B = ~ ((A → B) → ~ ( B → A))
                case BinaryConnective.BiConditional:
                    return new UnaryFormula(
                        new BinaryFormula(
                            new BinaryFormula(LHSFormula, RHSFormula, BinaryConnective.Implication, WorldIndex), 
                            new UnaryFormula(
                                new BinaryFormula(RHSFormula, LHSFormula, BinaryConnective.Implication, WorldIndex), 
                            UnaryConnective.Negation, WorldIndex), 
                         BinaryConnective.Implication, WorldIndex),
                    UnaryConnective.Negation, WorldIndex);

                default:
                    return this;
            }
        }
    }
}

