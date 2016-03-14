using System;
using System.Collections.Generic;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class FunctionDeclarationNode: AstNode
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public AstNode Body { get { return Children[0]; } }

        public FunctionDeclarationNode(string name, List<string> parameters, AstNode body)
        {
            Name = name;
            Parameters = parameters;
            Children.Add(body);
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

        public static FunctionDeclarationNode Parse(Parser parser)
        {
            string name = (string)parser.ExpectToken(TokenType.Identifier).Value;
            AstNode args = ArgListNode.Parse(parser);
            List<string> parameters = new List<string>();
            foreach (AstNode node in args.Children)
                parameters.Add(((IdentifierNode)node).Identifier);
            AstNode body = StatementNode.Parse(parser);

            return new FunctionDeclarationNode(name, parameters, body);
        }
    }
}