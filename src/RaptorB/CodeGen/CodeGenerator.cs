using System;

using RaptorB.Parser;
using RaptorB.SemanticAnalysis;

namespace RaptorB.CodeGen
{
    public class CodeGenerator : IVisitor
    {
        private AstNode ast;
        private SymbolTable symbolTable;

        public CodeGenerator(AstNode ast, SymbolTable symbolTable)
        {
            this.ast = ast;
            this.symbolTable = symbolTable;
        }

        public void Generate()
        {
            Console.WriteLine("Load_Immediate SP, 1000");
            ast.VisitChildren(this);
        }

        public void Accept(AutoNode node)
        {
        }
        public void Accept(ArgListNode node) {}
        public void Accept(BinaryOperationNode node) {}
        public void Accept(CharNode node)
        {
            Console.WriteLine("Push " + node.Char);
        }
        public void Accept(CodeBlockNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(FunctionDeclarationNode node) {}
        public void Accept(ConditionalNode node) {}
        public void Accept(ExpressionNode node) {}
        public void Accept(FunctionCallNode node)
        {
        }
        public void Accept(IdentifierNode node) {}
        public void Accept(NumberNode node) {}
        public void Accept(StatementNode node) {}
        public void Accept(WhileNode node) {}

        private const string putchar = ".putchar\n.putCharLoop\nPop a\nLoad_Byte b,a\nPrint_Char b\nInc a\nCmp_Immediate b,0\nJne putCharLoop\nRet";
    }
}