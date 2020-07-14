using Poz1.LogicProver.Model;
using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver
{
    public class Substitution : Dictionary<Variable, Terminal>
    {
        public List<Variable> Domain { get => Keys.ToList(); }
        public List<Terminal> Range { get => Values.ToList(); }

        public bool IsPure { get => !Range.Any(x => Domain.Contains(x)); }
        
        //Apply
    }
}