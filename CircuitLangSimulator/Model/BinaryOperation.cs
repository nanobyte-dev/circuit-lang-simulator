using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    internal class BinaryOperation : IExpression
    {
        public IExpression Left { get; set; }

        public IExpression Right { get; set; }

        public string Operator { get; set; }
    }
}
