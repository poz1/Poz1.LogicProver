namespace Poz1.LogicProver.Model
{
    public class TerminalEquation
    {
        public Terminal Terminal1 { get; set; }
        public Terminal Terminal2 { get; set; }

        public TerminalType Type { get; }

        public TerminalEquation(Terminal terminal1, Terminal terminal2)
        {
            Terminal1 = terminal1;
            Terminal2 = terminal2;
        }
    }

    public enum TerminalType
    {
        Function, Mixed
    }
}