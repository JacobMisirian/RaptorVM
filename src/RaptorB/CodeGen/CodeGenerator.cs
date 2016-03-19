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
            Console.WriteLine("Load_Immediate SP, 6000");
            Console.WriteLine("Mov BP, SP");
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
            for (int i = node.Children.Count - 1; i >= 0; i--)
            {
                node.Children[i].Visit(this);
                Console.WriteLine("Push " + popRegister());
            }
        }
        public void Accept(BinaryOperationNode node)
        {
            switch (node.BinaryOperation)
            {
                case BinaryOperation.Assignment:
                    Console.WriteLine("Mov " + pushRegister() + ", BP");
                    Console.WriteLine("Sub_Immediate " + getRegister() + ", " + (2 * (1 + symbolTable.GetIndex(((IdentifierNode)node.Left).Identifier))));
                    Console.WriteLine("Store_Word " + getRegister() + ", " + popRegister());
                    break;
                default:
                    throw new NotImplementedException("Unimplemented Binary Operation: " + node.BinaryOperation);
            }
        }
        public void Accept(CharNode node)
        {
            Console.WriteLine("Load_Immediate " + pushRegister() + ", " + Convert.ToInt16(node.Char));
        }
        public void Accept(CodeBlockNode node)
        {
            symbolTable.EnterScope();
            node.VisitChildren(this);
            symbolTable.PopScope();
        }
        public void Accept(FunctionDeclarationNode node)
        {
            symbolTable.EnterScope();
            Console.WriteLine("." + node.Name);
            Console.WriteLine("Push BP");
            Console.WriteLine("Mov BP, SP");
            Console.WriteLine("Sub_Immediate SP, " + symbolTable.GetGlobalIndex(node.Name) * 2);

            foreach (string param in node.Parameters)
            {
                symbolTable.AddSymbol(param);
                Console.WriteLine("Mov a, BP");
                Console.WriteLine("Add_Immediate a, " + (2 + symbolTable.GetIndex(param)) * 2);
                Console.WriteLine("Load_Word " + pushRegister() + ", a");

                Console.WriteLine("Mov a, BP");
                Console.WriteLine("Sub_Immediate a, " + (2 + symbolTable.GetIndex(param) * 2));
                Console.WriteLine("Store_Word a, " + popRegister());
            }

            node.VisitChildren(this);
            symbolTable.PopScope();
            Console.WriteLine("Add_Immediate SP, " + symbolTable.GetIndex(node.Name) * 2);
            Console.WriteLine("Pop BP");
            Console.WriteLine("Ret");
        }
        public void Accept(ConditionalNode node) {}
        public void Accept(ExpressionNode node) {}
        public void Accept(FunctionCallNode node)
        {
            node.Arguments.Visit(this);
            string call = ((IdentifierNode)node.Target).Identifier;
            Console.WriteLine("Call " + call);
            Console.WriteLine("Add_Immediate SP, " + node.Arguments.Children.Count * 2);
        }
        public void Accept(IdentifierNode node)
        {
            pushRegister();
            Console.WriteLine("Mov " + getRegister() + ", BP");
            Console.WriteLine("Sub_Immediate " + getRegister() + ", " + (2 + symbolTable.GetIndex(node.Identifier) * 2));
            Console.WriteLine("Load_Word " + popRegister() + ", " + ((char)++currentRegister).ToString());
        }
        public void Accept(NumberNode node)
        {
            Console.WriteLine("Load_Immediate " + pushRegister() + ", " + node.Number);
        }
        public void Accept(StatementNode node) {}
        public void Accept(WhileNode node) {}

        private const string putchar = ".putchar Push BP Mov BP, SP Mov a, BP Add_Immediate a, 4 Load_Byte b, a Print_Char b Pop BP Ret";

        private int currentRegister = (int)'b';

        private string pushRegister()
        {
            return ((char)currentRegister++).ToString();
        }

        private string popRegister()
        {
            return ((char)--currentRegister).ToString();
        }

        private string getRegister()
        {
            return ((char)currentRegister).ToString();
        }
    }
}