using System;
using System.Collections.Generic;
using System.Linq;

namespace RaptorB.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children = new List<AstNode>();
        public abstract object Visit(IVisitor visitor);
        public void VisitChild(IVisitor visitor)
        {
            Children.All(x =>
            {
                x.Visit(visitor);
                return true;
            });
        }
    }
}

