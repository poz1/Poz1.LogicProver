using Poz1.LogicProver.Model.Core;

namespace Poz1.LogicProver.Model.MGU
{
    public class Equation<T>
    {
        public T Terminal1 { get; set; }
        public T Terminal2 { get; set; }

        public Equation(T terminal1, T terminal2)
        {
            Terminal1 = terminal1;
            Terminal2 = terminal2;
        }
    }

    public class LogicEquation : Equation<LogicElement>
    {

    }
}