using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class AssignmentStatement : IStatement
    {
        public List<VariableReference> LHS { get; } = new List<VariableReference>();

        public List<IExpression> RHS { get; } = new List<IExpression>();
    }
}

