using System;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class StatementNode: AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier, "if"))
                return ConditionalNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "while"))
                return WhileNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "for"))
                return ForNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "auto"))
                return AutoNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "return"))
                return ReturnNode.Parse(parser);
            else
                return ExpressionStatementNode.Parse(parser);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}