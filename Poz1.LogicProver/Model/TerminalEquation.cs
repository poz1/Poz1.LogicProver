namespace Poz1.LogicProver.Model
{
    public class TerminalEquation
    {
        public Terminal terminal1 { get; }
        public Terminal terminal2 { get; }

        public TerminalType Type { get; }

        public TerminalEquation(Terminal terminal1, Terminal terminal2)
        {
            this.terminal1 = terminal1;
            this.terminal2 = terminal2;
        }
    }

    public enum TerminalType
    {
        Function, Mixed
    }
}