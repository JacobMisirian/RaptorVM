using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RaptorB.Parser;

namespace RaptorB.SemanticAnalysis
{
    public class SemanticAnalyzer : IVisitor
    {
        private AstNode code;
        private SymbolTable result;
        public SemanticAnalyzer(AstNode ast)
        {
            code = ast;
        }

        public SymbolTable Analyze()
        {
            result = new SymbolTable();
            result.EnterScope();
            code.VisitChildren(this);
            return result;
        }

        public void Accept(ArgListNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(AutoNode node)
        {
            foreach (string variable in node.Variables)
                result.AddSymbol(variable);
            node.VisitChildren(this);
        }
        public void Accept(BinaryOperationNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(CodeBlockNode node)
        {
            result.EnterScope();
            node.VisitChildren(this);
            result.PopScope();
        }
        public void Accept(FunctionDeclarationNode node)
        {
            result.AddSymbol(node.Name);
            result.EnterScope();
            foreach (string param in node.Parameters)
                result.AddSymbol(param);
            node.VisitChildren(this);
            result.PopScope(node.Name);
        }
        public void Accept(UnaryOperationNode node) {}
        public void Accept(IdentifierNode node) {}
        public void Accept(NumberNode node) {}
        public void Accept(CharNode node) {}
        public void Accept(ConditionalNode node) {}
        public void Accept(FunctionCallNode node) {}
        public void Accept(ExpressionNode node) {}
        public void Accept(ExpressionStatementNode node) {}
        public void Accept(StatementNode node) {}
        public void Accept(StringNode node) {}
        public void Accept(WhileNode node) {}
    }
}