using System;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class AutoNode: AstNode
    {
        public string Variable { get; private set; }

        public AutoNode(string variable)
        {
            Variable = variable;
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

        public static AutoNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "auto");
            string variable = (string)parser.ExpectToken(TokenType.Identifier).Value;
            return new AutoNode(variable);
        }
    }
}

