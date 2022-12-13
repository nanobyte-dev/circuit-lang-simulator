using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class Module
    {
        public string Name { get; set; }

        public bool IsMain { get; set; }

        public List<VariableReference> Inputs { get; } = new List<VariableReference>();

        public List<VariableReference> Outputs { get; } = new List<VariableReference>();

        public List<IStatement> Statements { get; } = new List<IStatement>();
    }
}
