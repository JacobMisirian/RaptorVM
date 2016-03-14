using System;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class WhileNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }

        public WhileNode(AstNode predicate, AstNode body)
        {
            Children.Add(predicate);
            Children.Add(body);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }

        public static WhileNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "while");
            AstNode predicate = ExpressionNode.Parse(parser);
            AstNode body = StatementNode.Parse(parser);
            return new WhileNode(predicate, body);
        }
    }
}

