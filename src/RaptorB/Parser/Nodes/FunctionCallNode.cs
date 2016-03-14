using System;
using System.Collections.Generic;

namespace RaptorB.Parser
{
    public class FunctionCallNode: AstNode
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }

        public FunctionCallNode(string name, List<string> parameters)
        {
            Name = name;
            Parameters = parameters;
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

