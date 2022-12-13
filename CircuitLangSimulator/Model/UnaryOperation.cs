using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class UnaryOperation : IExpression
    {
        public IExpression Left { get; set; }

        public string Operator { get; set; }
    }
}
