using System;
using System.Collections.Generic;
using System.Linq;

namespace RaptorB.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children = new List<AstNode>();
        public abstract void Visit(IVisitor visitor);
        public abstract void VisitChildren(IVisitor visitor);
    }
}

