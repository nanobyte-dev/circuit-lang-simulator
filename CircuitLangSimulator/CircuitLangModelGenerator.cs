using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CircuitLangSimulator.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitLangSimulator
{
    public class CircuitLangModelGenerator : CircuitLangBaseVisitor<object>
    {
        public override object VisitProgram([NotNull] CircuitLangParser.ProgramContext context)
        {
            var program = new Program();
            foreach (var module in context.module())
                program.Modules.Add((Module)Visit(module));
            return program;
        }

        public override object VisitModule([NotNull] CircuitLangParser.ModuleContext context)
        {
            var module = new Module();

            module.IsMain = context.MAIN() != null;
            module.Name = context.IDENTIFIER().GetText();

            foreach (var input in context.module_argument_list().identifier_with_array())
            {
                var varRef = (VariableReference)Visit(input);
                if (varRef.Index == 0)
                    varRef.Index = 1;

                module.Inputs.Add(varRef);
            }

            foreach (var ouput in context.module_output_list().identifier_with_array())
            {
                var varRef = (VariableReference)Visit(ouput);
                if (varRef.Index == 0)
                    varRef.Index = 1;

                module.Outputs.Add(varRef);
            }

            foreach (var stmt in context.statement())
                module.Statements.Add((IStatement)Visit(stmt));

            return module;
        }

        public override object VisitStatement([NotNull] CircuitLangParser.StatementContext context)
        {
            if (context.assignment() != null)
                return Visit(context.assignment());

            else return Visit(context.variable_declaration());
        }

        public override object VisitAssignment([NotNull] CircuitLangParser.AssignmentContext context)
        {
            var assignment = new AssignmentStatement();

            foreach (var lhs in context.identifier_with_array())
                assignment.LHS.Add((VariableReference)Visit(lhs));

            foreach (var rhs in context.expression())
                assignment.RHS.Add((IExpression)Visit(rhs));

            return assignment;
        }

        public override object VisitExpression([NotNull] CircuitLangParser.ExpressionContext context)
        {
            // identifier_with_array
            if (context.identifier_with_array() != null)
                return Visit(context.identifier_with_array());

            // LITERAL
            if (context.literal() != null)
                return Visit(context.literal());

            // '(' expression ')'
            if (context.GetChild(0) is ITerminalNode terminalNode && terminalNode.GetText() == "(")
                return Visit(context.GetChild(1));

            // expression binary_operator expression
            if (context.expression().Length == 2)
                return new BinaryOperation()
                {
                    Left = (IExpression)Visit(context.expression()[0]),
                    Operator = context.binary_operator().GetText(),
                    Right = (IExpression)Visit(context.expression()[1]),
                };

            // unary_operation
            if (context.unary_operation() != null)
                return Visit(context.unary_operation());

            // module_call
            if (context.module_call() != null)
                return Visit(context.module_call());

            // module_call
            if (context.truth_table() != null)
                return Visit(context.truth_table());

            return base.VisitExpression(context);
        }

        public override object VisitUnary_operation([NotNull] CircuitLangParser.Unary_operationContext context)
        {
            return new UnaryOperation()
            {
                Left = (IExpression)Visit(context.GetChild(1)),
                Operator = context.GetChild(0).GetText()
            };
        }

        public override object VisitIdentifier_with_array([NotNull] CircuitLangParser.Identifier_with_arrayContext context)
        {
            return new VariableReference()
            {
                VariableName = context.IDENTIFIER().GetText(),
                Index = context.DEC_NUMBER() == null ? 0 : ((Literal)Visit(context.DEC_NUMBER())).Value
            };
        }

        public override object VisitTerminal(ITerminalNode node)
        {
            switch (node.Symbol.Type)
            {
                case CircuitLangLexer.DEC_NUMBER:
                    return new Literal() { Value = int.Parse(node.GetText()) };

                case CircuitLangLexer.HEX_NUMBER:
                    return new Literal()
                    {
                        Value = Convert.ToInt32(node.GetText().Substring(2), 16)
                    };

                case CircuitLangLexer.OCT_NUMBER:
                    return new Literal()
                    {
                        Value = Convert.ToInt32(node.GetText().Substring(2), 8)
                    };

                case CircuitLangLexer.BIN_NUMBER:
                    return new Literal()
                    {
                        Value = Convert.ToInt32(node.GetText().Substring(2), 2)
                    };

                default:
                    return base.VisitTerminal(node);
            }
        }

        public override object VisitModule_call([NotNull] CircuitLangParser.Module_callContext context)
        {
            var moduleCall = new ModuleCall()
            {
                ModuleName = context.IDENTIFIER().GetText(),
            };

            foreach (var expr in context.expression())
                moduleCall.Inputs.Add((IExpression)Visit(expr));

            return moduleCall;
        }

        public override object VisitTruth_table([NotNull] CircuitLangParser.Truth_tableContext context)
        {
            var truthTable = new TruthTable();
            
            foreach (var input in context.expression())
                truthTable.Inputs.Add((IExpression)Visit(input));

            foreach (var entry in context.truth_table_entry())
                truthTable.Entries.Add((TruthTableEntry)Visit(entry));

            return truthTable;
        }

        public override object VisitTruth_table_entry([NotNull] CircuitLangParser.Truth_table_entryContext context)
        {
            int[] inputs = new int[context.literal().Length];
            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = ((Literal)Visit(context.literal(i))).Value;

            int?[] outputs = new int?[context.literal_or_wild().Length];
            for (int i = 0; i < outputs.Length; i++)
            {
                if (context.literal_or_wild(i).GetText() == "?")
                    outputs[i] = null;

                else outputs[i] = ((Literal)Visit(context.literal_or_wild(i))).Value;
            }

            return new TruthTableEntry() { Inputs = inputs, Outputs = outputs };
        }

        public override object VisitVariable_declaration([NotNull] CircuitLangParser.Variable_declarationContext context)
        {
            var variableReference = (VariableReference)Visit(context.identifier_with_array());

            return new VariableDeclarationStatement()
            {
                VariableName = variableReference.VariableName,
                Width = variableReference.Index
            };
        }
    }
}
