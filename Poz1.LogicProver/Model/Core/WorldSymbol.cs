using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class WorldSymbol : ILogicElement
    {
        internal LogicElement BaseElement { get; set; }
        public WorldSymbol ParentSymbol { get; internal set; }
        public abstract bool IsGround { get; }

        LogicElement ILogicElement.ToLogicElement()
        {
            return BaseElement;
        }

        public abstract Terminal ToTerminal();
    }

    public abstract class WorldSymbol<T> : WorldSymbol where T : LogicElement
    {
        public WorldSymbol(T baseElement)
        { 
            BaseElement = baseElement;
        }

        public override string ToString()
        {
            return BaseElement.ToString();
        }
    }

    public class ConstantWorldSymbol : WorldSymbol<Constant>
    {
        public ConstantWorldSymbol(string value) : base(new Constant(value))
        {
        }

        public override bool IsGround => true;

        public override string ToString()
        {
            return BaseElement.Name;
        }

        public override Terminal ToTerminal()
        {
            return new ConstantTerminal(BaseElement.Name);
        }
    }

    public class VariableWorldSymbol : WorldSymbol<Variable>
    {
        public VariableWorldSymbol(string value) : base(new Variable(value))
        {
        }

        public override bool IsGround => false;

        public override string ToString()
        {
            return BaseElement.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is WorldSymbol symbol && symbol.BaseElement.Name == BaseElement.Name)
                return true;

            return false;
        }
        public override Terminal ToTerminal()
        {
            return new  VariableTerminal(BaseElement.Name);
        }
    }

    public class FunctionWorldSymbol : WorldSymbol<Function<WorldSymbol>>
    {
        public int Arity => ((Function<WorldSymbol>)BaseElement).Parameters.Count();

        public override bool IsGround => false ;

        public FunctionWorldSymbol(string value, IList<WorldSymbol> parameters) :
            base(new Function<WorldSymbol>(value, (List<WorldSymbol>)parameters))
        {
        }

        public FunctionWorldSymbol(string value, params WorldSymbol[] parameters) :
           base(new Function<WorldSymbol>(value, parameters))
        {
        }

        public override Terminal ToTerminal()
        {
            return new FunctionTerminal(BaseElement.Name, 
                ((Function<WorldSymbol>)BaseElement).Parameters.Select(x => x.ToTerminal()).ToList()); 
        }
    }
}