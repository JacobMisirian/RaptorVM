using System;

using RaptorB.Parser;
using RaptorB.SemanticAnalysis;
using RaptorB.Interpreter;

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
            Console.WriteLine("Call main");
            Console.WriteLine(".hang Jmp hang");
            ast.VisitChildren(this);
            Console.WriteLine(putchar);
        }

        public void Accept(AutoNode node)
        {
        }
        public void Accept(ArgListNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(BinaryOperationNode node) {}
        public void Accept(CharNode node)
        {
            Console.WriteLine("Push_Immediate " + Convert.ToInt16(node.Char));
        }
        public void Accept(CodeBlockNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(FunctionDeclarationNode node)
        {
            Console.WriteLine("." + node.Name);
            node.VisitChildren(this);
            Console.WriteLine("Ret");
        }
        public void Accept(ConditionalNode node) {}
        public void Accept(ExpressionNode node) {}
        public void Accept(FunctionCallNode node)
        {
            node.Arguments.Visit(this);
            string call = ((IdentifierNode)node.Target).Identifier;
            Console.WriteLine("Call " + call);
        }
        public void Accept(IdentifierNode node) {}
        public void Accept(NumberNode node) {}
        public void Accept(StatementNode node) {}
        public void Accept(WhileNode node) {}

        private const string putchar = ".putchar Pop a Pop b Print_Char b Push a Ret";
    }
}