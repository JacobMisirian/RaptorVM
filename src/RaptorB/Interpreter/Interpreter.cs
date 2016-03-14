using System;
using System.Collections.Generic;
using System.Reflection;

using RaptorB.Lexer;
using RaptorB.Parser;

namespace RaptorB.Interpreter
{
    public class Interpreter
    {
        private AstNode code;
        private int position;
        private Dictionary<string, object> variables = new Dictionary<string, object>();

        public Interpreter()
        {
            foreach (Dictionary<string, InternalFunction> entries in getFunctions())
                foreach (KeyValuePair<string, InternalFunction> entry in entries)
                    variables.Add(entry.Key, entry.Value);
        }

        public void Interpret(AstNode tree)
        {
            code = tree;
            Console.WriteLine(code.Children.Count);
            for (position = 0; position < code.Children.Count; position++)
                executeNode(code.Children[position]);
        }

        private void executeNode(AstNode node)
        {
            if (node is CodeBlockNode)
                foreach (AstNode subNode in ((CodeBlockNode)node).Children)
                    executeNode(subNode);
            else if (node is ConditionalNode)
            {
                ConditionalNode conditionalNode = (ConditionalNode)node;
                if ((bool)evaluateNode(conditionalNode.Predicate))
                    executeNode(conditionalNode.Body);
                else if (conditionalNode.Children.Count >= 3)
                    executeNode(conditionalNode.ElseBody);
            }
            else if (node is WhileNode)
            {
                WhileNode whileNode = (WhileNode)node;
                while ((bool)evaluateNode(whileNode.Predicate))
                    executeNode(whileNode.Body);
            }
            else
                evaluateNode(node);
        }

        private object evaluateNode(AstNode node)
        {
            if (node is BinaryOperationNode)
                return interpretBinaryOperation((BinaryOperationNode)node);
            else if (node is IdentifierNode)
            {
                IdentifierNode idNode = (IdentifierNode)node;
                if (variables.ContainsKey(idNode.Identifier))
                    return variables[idNode.Identifier];
                throw new Exception("Variable " + idNode.Identifier + " does not exist in dictionary!");
            }
            else if (node is FunctionCallNode)
            {
                FunctionCallNode fnode = (FunctionCallNode)node;
                IFunction target = evaluateNode(fnode.Target) as IFunction;
                if (target == null)
                    throw new Exception("Attempt to run a non-valid function!");
                object[] arguments = new object[fnode.Arguments.Children.Count];
                for (int x = 0; x < fnode.Arguments.Children.Count; x++)
                    arguments[x] = evaluateNode(fnode.Arguments.Children[x]);
                return target.Invoke(arguments);
            }
            else if (node is NumberNode)
                return ((NumberNode)node).Number;
            else if (node is CharNode)
                return ((CharNode)node).Char;
            else
                throw new Exception("Unknown node: " + node);
        }

        private object interpretBinaryOperation(BinaryOperationNode node)
        {
            switch (node.BinaryOperation)
            {
                case BinaryOperation.Assignment:
                    string variable = ((IdentifierNode)node.Left).Identifier;
                    object value = evaluateNode(node.Right);
                    if (variables.ContainsKey(variable))
                        variables.Remove(variable);
                    variables.Add(variable, value);
                    return value;
                case BinaryOperation.Addition:
                    return Convert.ToInt16(evaluateNode(node.Left)) + Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.Subtraction:
                    return Convert.ToInt16(evaluateNode(node.Left)) - Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.Multiplication:
                    return Convert.ToInt16(evaluateNode(node.Left)) * Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.Division:
                    return Convert.ToInt16(evaluateNode(node.Left)) / Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.Modulus:
                    return Convert.ToInt16(evaluateNode(node.Left)) % Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.EqualTo:
                    return Convert.ToInt16(evaluateNode(node.Left)) == Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.NotEqualTo:
                    return Convert.ToInt16(evaluateNode(node.Left)) != Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.GreaterThan:
                    return Convert.ToInt16(evaluateNode(node.Left)) > Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.GreaterThanOrEqual:
                    return Convert.ToInt16(evaluateNode(node.Left)) >= Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.LessThan:
                    return Convert.ToInt16(evaluateNode(node.Left)) < Convert.ToInt16(evaluateNode(node.Right));
                case BinaryOperation.LessThanOrEqual:
                    return Convert.ToInt16(evaluateNode(node.Left)) <= Convert.ToInt16(evaluateNode(node.Right));
                default:
                    throw new Exception("Unknown Binary Operation: " + node.BinaryOperation);
            }
        }

        private List<Dictionary<string, InternalFunction>> getFunctions(string path = "")
        {
            List<Dictionary<string, InternalFunction>> result = new List<Dictionary<string, InternalFunction>>();
            Assembly testAss;

            if (path != "")
                testAss = Assembly.LoadFrom(path);
            else
                testAss = Assembly.GetExecutingAssembly();

            foreach (Type type in testAss.GetTypes())
                if (type.GetInterface(typeof(ILibrary).FullName) != null)
            {
                ILibrary ilib = (ILibrary)Activator.CreateInstance(type);
                result.Add(ilib.GetFunctions());
            }

            return result;
        }
    }
}