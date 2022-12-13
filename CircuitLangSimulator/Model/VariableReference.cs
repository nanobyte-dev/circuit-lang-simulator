using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class VariableReference : IExpression
    {
        public string VariableName { get; set; }
        
        public int Index { get; set; } = 0;
    }
}
