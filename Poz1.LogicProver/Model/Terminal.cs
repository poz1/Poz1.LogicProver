using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Poz1.LogicProver.Model
{
    public abstract class Terminal
    {
        public string Value { get; set; }
        public abstract IList<Variable> Variables { get; }

        //internal Substitution FindMGU(Terminal terminal);
    }

    public class Constant : Terminal
    {
        public override IList<Variable> Variables { get => new List<Variable>(); }

        //internal override Substitution FindMGU(Terminal terminal)
        //{
        //    if (Value == terminal.Value)
        //    {

        //    }
        //}
    }

    public class Variable : Terminal
    {
        public override IList<Variable> Variables { get => new List<Variable>() { this }; }

        //internal override string FindMGU(Substitution sub, Terminal terminal)
        //{
        //    throw new System.NotImplementedException();
        //}
    }

    public class Function : Terminal
    {
        public IList<Terminal> Parameters { get; set; }
        public int Arity => Variables.Count();

        public override IList<Variable> Variables { get => ComputeVariables();}

        //internal override string FindMGU(Substitution sub, Terminal terminal)
        //{
        //    var function = (Function)terminal;

        //    if(Arity != function.Arity)
        //        throw new System.Exception("No MGU: Different Arity");

        //    if (Value != function.Value)
        //        throw new System.Exception("No MGU: Different Function");

        //    foreach (var param in Parameters)
        //    {
        //        param.FindUnification(function.Parameters[Parameters])
        //    }
        //}

        private IList<Variable> ComputeVariables()
        {
            var vars = new List<Variable>();
            foreach (var terminal in Parameters)
            {
                vars.AddRange(terminal.Variables);
            }

            return vars;
        }
    }
}