using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator.Model
{
    public class Node
    {
        public List<Node> Inputs { get; } = new List<Node>();

        public List<Node> Outputs { get; } = new List<Node>();

        public int Width { get; set; } = 1;

        public BitArray Value { get; set; }

        public virtual void Visit()
        {
            if (Inputs.Count <= 0)
                throw new Exception("Node is missing inputs!");

            foreach (var output in Outputs)
                output.Value = Inputs.First().Value;
        }
    }
}
