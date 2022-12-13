using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class TruthTable : IExpression
    {
        public List<IExpression> Inputs { get; } = new List<IExpression>();

        public List<TruthTableEntry> Entries { get; } = new List<TruthTableEntry>();
    }
}
