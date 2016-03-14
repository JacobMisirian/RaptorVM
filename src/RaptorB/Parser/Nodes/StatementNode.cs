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
            else
                return ExpressionNode.Parse(parser);
        }
    }
}

