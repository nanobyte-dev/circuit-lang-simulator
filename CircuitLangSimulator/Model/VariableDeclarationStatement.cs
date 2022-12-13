using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class VariableDeclarationStatement : IStatement
    {
        public string VariableName { get; set; }

        public int Width { get; set; }
    }
}
