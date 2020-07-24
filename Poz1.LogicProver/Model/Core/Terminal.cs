using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Terminal : ILogicElement
    {
        internal LogicElement BaseElement { get; set; }
        public abstract List<VariableTerminal> FreeVariables { get; }

        LogicElement ILogicElement.ToLogicElement()
        {
            return BaseElement;
        }

        public abstract WorldSymbol ToWorldSymbol();
    }

    public abstract class Terminal<T> : Terminal where T : LogicElement
    {
        public Terminal(T baseElement)
        {
            BaseElement = baseElement;
        }

        public override string ToString()
        {
            return BaseElement.ToString();
        }
    }

    public class ConstantTerminal : Terminal<Constant>
    {
        public ConstantTerminal(string value) : base(new Constant(value))
        {
        }

        public override List<VariableTerminal> FreeVariables { get => new List<VariableTerminal>(); }

        public override WorldSymbol ToWorldSymbol()
        {
            return new ConstantWorldSymbol(BaseElement.Name);
        }
    }

    public class VariableTerminal : Terminal<Variable>
    {
        public VariableTerminal(string value) : base(new Variable(value))
        {
        }

        public override List<VariableTerminal> FreeVariables { get => new List<VariableTerminal>() { this }; }

        public override WorldSymbol ToWorldSymbol()
        {
            return new VariableWorldSymbol(BaseElement.Name);
        }
    }

    public class FunctionTerminal : Terminal<Function<Terminal>>
    {
        public int Arity => ((Function<Terminal>)BaseElement).Arity;

        public override List<VariableTerminal> FreeVariables { get => ComputeVariables();}
        public IEnumerable<Terminal> Parameters { get => ((Function<Terminal>)BaseElement).Parameters; }

        public FunctionTerminal(string value, IList<Terminal> parameters) : 
            base(new Function<Terminal>(value, (List<Terminal>)parameters))
        {
        }

        public FunctionTerminal(string value, IList<ILogicElement> parameters) :
           base(new Function<Terminal>(value, (List<Terminal>)parameters))
        {
        }

        public FunctionTerminal(string value, params Terminal[] parameters) :
           base(new Function<Terminal>(value, parameters))
        {
        }

        public override WorldSymbol ToWorldSymbol()
        {
            return new FunctionWorldSymbol(BaseElement.Name,
                ((Function<Terminal>)BaseElement).Parameters.Select(x => x.ToWorldSymbol()).ToList());
        }

        private List<VariableTerminal> ComputeVariables()
        {
            var vars = new List<VariableTerminal>();
            foreach (var terminal in ((Function<Terminal>)BaseElement).Parameters)
            {
                vars.AddRange(terminal.FreeVariables);
            }
            return vars;
        }
    }
}