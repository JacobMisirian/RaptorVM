using System;
using System.Collections.Generic;

namespace RaptorB.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children = new List<AstNode>();
    }
}

