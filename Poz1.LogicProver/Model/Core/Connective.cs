namespace Poz1.LogicProver.Model.Core
{
    public static class UnaryConnective
    {
        public const string Negation = "~";
        public const string Necessity = "◻";
        public const string Possibility = "◊";
    }

    public static class BinaryConnective
    {
        public const string Conjunction = "∧";
        public const string Disjunction = "∨";
        public const string Implication = "→";
        public const string BiConditional = "↔";
    }

    public static class QuantifierConnective
    {
        public const string Exist = "∃";
        public const string ForAll = "∀";
    }
}
