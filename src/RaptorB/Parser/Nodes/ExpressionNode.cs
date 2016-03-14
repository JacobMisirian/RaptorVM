using System;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class ExpressionNode: AstNode
    {
        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }

        public static AstNode Parse(Parser parser)
        {
            return parseAssignment(parser);
        }

        private static AstNode parseAssignment(Parser parser)
        {
            AstNode left = parseAdditive(parser);

            if (parser.AcceptToken(TokenType.Assignment))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, parseAssignment(parser));
            else if (parser.AcceptToken(TokenType.Comparison, "=="))
                return new BinaryOperationNode(BinaryOperation.EqualTo, left, parseAssignment(parser));
            else
                return left;
        }

        private static AstNode parseAdditive(Parser parser)
        {
            AstNode left = parseMultiplicitive(parser);
            while (parser.MatchToken(TokenType.Operation))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case "+":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOperationNode(BinaryOperation.Addition, left, parseMultiplicitive(parser));
                        continue;
                    case "-":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOperationNode(BinaryOperation.Subtraction, left, parseMultiplicitive(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseMultiplicitive(Parser parser)
        {
            AstNode left = parseComparison(parser);
            while (parser.MatchToken(TokenType.Operation))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case "*":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOperationNode(BinaryOperation.Multiplication, left, parseComparison(parser));
                        continue;
                    case "/":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOperationNode(BinaryOperation.Division, left, parseComparison(parser));
                        continue;
                    case "%":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOperationNode(BinaryOperation.Modulus, left, parseComparison(parser));
                        continue;
                    case "=":
                        parser.AcceptToken(TokenType.Assignment);
                        left = new BinaryOperationNode(BinaryOperation.Assignment, left, parseComparison(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseComparison(Parser parser)
        {
            AstNode left = parseFunctionCall(parser);
            while (parser.MatchToken(TokenType.Comparison))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case ">":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.GreaterThan, left, parseFunctionCall(parser));
                        continue;
                    case "<":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.LessThan, left, parseFunctionCall(parser));
                        continue;
                    case ">=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.GreaterThanOrEqual, left, parseFunctionCall(parser));
                        continue;
                    case "<=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.LessThanOrEqual, left, parseFunctionCall(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseFunctionCall(Parser parser)
        {
            return parseFunctionCall(parser, parseTerm(parser));
        }
        private static AstNode parseFunctionCall(Parser parser, AstNode left)
        {
            if (parser.MatchToken(TokenType.Parentheses, "("))
                return parseFunctionCall(parser, new FunctionCallNode(left, ArgListNode.Parse(parser)));
            else
                return left;
        }

        private static AstNode parseTerm(Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier))
                return new IdentifierNode((string)parser.ExpectToken(TokenType.Identifier).Value);
            else if (parser.MatchToken(TokenType.Number))
                return new NumberNode((Int16)parser.ExpectToken(TokenType.Number).Value);
            else if (parser.MatchToken(TokenType.Char))
                return new CharNode((char)parser.ExpectToken(TokenType.Char).Value);
            else if (parser.AcceptToken(TokenType.Bracket, "{"))
            {
                CodeBlockNode block = new CodeBlockNode();
                while (!parser.AcceptToken(TokenType.Bracket, "}"))
                    block.Children.Add(StatementNode.Parse(parser));
                return block;
            }
            else if (parser.AcceptToken(TokenType.Parentheses, "("))
            {
                AstNode expression = ExpressionNode.Parse(parser);
                parser.ExpectToken(TokenType.Parentheses, ")");
                return expression;
            }
            else
                throw new Exception("Unknown token " + parser.CurrentToken().TokenType);
        }
    }
}

