using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class ModuleCall : IExpression
    {
        public string ModuleName { get; set; }

        public List<IExpression> Inputs { get; } = new List<IExpression>();
    }
}
