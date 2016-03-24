using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaptorB.Parser
{
    public interface IVisitor
    {
        void Accept(ArgListNode node);
        void Accept(AutoNode node);
        void Accept(BinaryOperationNode node);
        void Accept(CharNode node);
        void Accept(CodeBlockNode node);
        void Accept(ConditionalNode node);
        void Accept(ExpressionNode node);
        void Accept(ExpressionStatementNode node);
        void Accept(ForNode node);
        void Accept(FunctionDeclarationNode node);
        void Accept(FunctionCallNode node);
        void Accept(IdentifierNode node);
        void Accept(NumberNode node);
        void Accept(ReturnNode node);
        void Accept(StatementNode node);
        void Accept(StringNode node);
        void Accept(WhileNode node);
        void Accept(UnaryOperationNode node);
    }
}
