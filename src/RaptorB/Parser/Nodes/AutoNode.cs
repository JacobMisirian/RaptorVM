using System;
using System.Collections.Generic;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class AutoNode: AstNode
    {
        public List<string> Variables { get; private set; }

        public AutoNode(List<string> variables)
        {
            Variables = variables;
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
            List<string> variables = new List<string>();
            variables.Add((string)parser.ExpectToken(TokenType.Identifier).Value);
            while (parser.AcceptToken(TokenType.Comma))
                variables.Add((string)parser.ExpectToken(TokenType.Identifier).Value);
            return new AutoNode(variables);
        }
    }
}

