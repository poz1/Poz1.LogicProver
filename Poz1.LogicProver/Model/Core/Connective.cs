namespace Poz1.LogicProver.Model.Core
{
    public static class UnaryConnective
    {
        public static string Negation = "~";
        public static string Necessity = "◻";
        public static string Possibility = "◊";
    }

    public static class BinaryConnective
    {
        public static string Conjunction = "∧";
        public static string Disjunction = "∨";
        public static string Implication = "→";
        public static string Equivalence = "=";
    }

    public static class QuantifierConnective
    {
        public static string Exist = "∃";
        public static string ForAll = "∀";
    }
}
