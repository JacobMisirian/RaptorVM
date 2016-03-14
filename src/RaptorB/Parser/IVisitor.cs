using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaptorB.Parser
{
    public interface IVisitor
    {
        object Accept(ArgListNode node);
        object Accept(BinaryOperationNode node);
        object Accept(CharNode node);
        object Accept(CodeBlockNode node);
        object Accept(ConditionalNode node);
        object Accept(ExpressionNode node);
        object Accept(FunctionCallNode node);
        object Accept(IdentifierNode node);
        object Accept(NumberNode node);
        object Accept(StatementNode node);
        object Accept(WhileNode node);
    }
}
