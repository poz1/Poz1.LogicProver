using System.Collections.Generic;

namespace Poz1.LogicProver.Model
{
    public abstract class Formula
    {    
      public IList<WorldSymbol> WorldIndex { get; set; }
    }

    public class AtomicFormula : Formula
    {
        public string Terminal { get; set; }
    }

    public class UnaryFormula : Formula
    {
        public UnaryConnective Connective { get; set; }
        public Formula Formula { get; set; }
    }

    public class BinaryFormula : Formula
    {
        public BinaryConnective Connective { get; set; }
        public Formula LHSFormula { get; set; }
        public Formula RHSFormula { get; set; }
    }
}
