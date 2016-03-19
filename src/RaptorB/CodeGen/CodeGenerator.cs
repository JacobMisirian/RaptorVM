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
            Console.WriteLine(putint);
        }

        public void Accept(AutoNode node)
        {
            foreach (string variable in node.Variables)
                symbolTable.AddSymbol(variable);
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
            string registerOne, registerTwo, conditionSymbol, endSymbol;
            switch (node.BinaryOperation)
            {
                case BinaryOperation.Assignment:
                    node.Right.Visit(this);
                    Console.WriteLine("Mov a, BP");
                    Console.WriteLine("Sub_Immediate a, " + (2 + symbolTable.GetIndex(((IdentifierNode)node.Left).Identifier) * 2));
                    Console.WriteLine("Store_Word a, " + popRegister());
                    pushRegister();
                    break;
                case BinaryOperation.Addition:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    Console.WriteLine("Add " + registerTwo + ", " + registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Subtraction:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    Console.WriteLine("Sub " + registerTwo + ", " + registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Multiplication:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    Console.WriteLine("Mul " + registerTwo + ", " + registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Division:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    Console.WriteLine("Div " + registerTwo + ", " + registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Modulus:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    Console.WriteLine("Mod " + registerTwo + ", " + registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.GreaterThan:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    conditionSymbol = "symbol" + nextSymbol++;
                    endSymbol = "symbol" + nextSymbol++;
                    Console.WriteLine("Cmp " + registerTwo + ", " + registerOne);
                    Console.WriteLine("Jg " + conditionSymbol);
                    Console.WriteLine("Jmp " + endSymbol);
                    Console.WriteLine("." + conditionSymbol);
                    Console.WriteLine("Cmp " + registerTwo + ", " + registerTwo);
                    Console.WriteLine("." + endSymbol);
                    pushRegister();
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
        public void Accept(ConditionalNode node)
        {
            node.Predicate.Visit(this);
            string elseSymbol = "symbol" + nextSymbol++;
            string endSymbol = "symbol" + nextSymbol++;
            Console.WriteLine("Jne " + elseSymbol);
            node.Body.Visit(this);
            Console.WriteLine("Jmp " + endSymbol);
            Console.WriteLine("." + elseSymbol);
            node.ElseBody.Visit(this);
            Console.WriteLine("." + endSymbol);

        }
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
        private const string putint = ".putint Push BP Mov BP, SP Mov a, BP Add_Immediate a, 4 Load_Byte b, a Print b Pop BP Ret";

        private int currentRegister = (int)'b';
        private int nextSymbol = 0;

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