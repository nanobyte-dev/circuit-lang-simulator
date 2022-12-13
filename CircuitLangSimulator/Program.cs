using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CircuitLangSimulator
{
    internal class Program2
    {
        static void Main(string[] args)
        {
            using (var source = File.OpenRead(args[0]))
            {
                ICharStream stream = CharStreams.fromStream(source);
                ITokenSource lexer = new CircuitLangLexer(stream);
                ITokenStream tokens = new CommonTokenStream(lexer);
                CircuitLangParser parser = new CircuitLangParser(tokens);
                parser.BuildParseTree = true;
                IParseTree tree = parser.program();

            }
        }
    }
}
