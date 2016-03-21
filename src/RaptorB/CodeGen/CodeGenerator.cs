using System;
using System.Text;

using RaptorB.Parser;
using RaptorB.SemanticAnalysis;

namespace RaptorB.CodeGen
{
    public class CodeGenerator : IVisitor
    {
        private AstNode ast;
        private SymbolTable symbolTable;
        private StringBuilder result;

        public CodeGenerator(AstNode ast, SymbolTable symbolTable)
        {
            this.ast = ast;
            this.symbolTable = symbolTable;
        }

        public string Generate()
        {
            result = new StringBuilder();
            append("Load_Immediate SP, 6000");
            append("Mov BP, SP");
            append("Call main");
            append(".hang Jmp hang");
            ast.VisitChildren(this);
            append(putchar);
            append(putint);
            return result.ToString();
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
                append("Push {0}", popRegister());
            }
        }
        public void Accept(BinaryOperationNode node)
        {
            string registerOne, registerTwo;
            switch (node.BinaryOperation)
            {
                case BinaryOperation.Assignment:
                    node.Right.Visit(this);
                    append("Mov a, BP");
                    append("Sub_Immediate a, {0}", (2 + symbolTable.GetIndex(((IdentifierNode)node.Left).Identifier) * 2));
                    append("Store_Word a, {0}", popRegister());
                    pushRegister();
                    break;
                case BinaryOperation.Addition:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Add {0}, {1}", registerTwo, registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Subtraction:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Sub {0}, {1}", registerTwo, registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Multiplication:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Mul {0}, {1}", registerTwo, registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Division:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Div {0}, {1}", registerTwo, registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.Modulus:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Mod {0}, {1}", registerTwo, registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.EqualTo:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Cmp {0}, {1}", registerTwo, registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.NotEqualTo:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Cmp {0}, {1}", registerTwo, registerOne);
                    append("Not FLAGS");
                    pushRegister();
                    break;
                case BinaryOperation.GreaterThan:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Cmp {0}, {1}", registerTwo, registerOne);
                    append("Mov {0}, FLAGS", registerOne);
                    append("And_Immediate {0}, 2", registerOne);
                    append("Xor_Immediate {0}, 2", registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.GreaterThanOrEqual:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Cmp {0}, {1}", registerTwo, registerOne);
                    append("Mov {0}, FLAGS", registerOne);
                    append("And_Immediate {0}, 3", registerOne);
                    append("Xor_Immediate {0}, 2", registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.LessThan:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Cmp {0}, {1}", registerTwo, registerOne);
                    append("Mov {0}, FLAGS", registerOne);
                    append("And_Immediate {0}, 3", registerOne);
                    pushRegister();
                    break;
                case BinaryOperation.LessThanOrEqual:
                    node.Right.Visit(this);
                    node.Left.Visit(this);
                    registerOne = popRegister();
                    registerTwo = popRegister();
                    append("Cmp {0}, {1}", registerTwo, registerOne);
                    append("Mov {0}, FLAGS", registerOne);
                    append("And_Immediate {0}, 2", registerOne);
                    pushRegister();
                    break;
                default:
                    throw new NotImplementedException("Unimplemented Binary Operation: " + node.BinaryOperation);
            }
        }
        public void Accept(CharNode node)
        {
            append("Load_Immediate " + pushRegister() + ", " + Convert.ToInt16(node.Char));
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
            append("." + node.Name);
            append("Push BP");
            append("Mov BP, SP");
            append("Sub_Immediate SP, {0}", symbolTable.GetGlobalIndex(node.Name) * 2);

            foreach (string param in node.Parameters)
            {
                symbolTable.AddSymbol(param);
                append("Mov a, BP");
                append("Add_Immediate a, {0}", (2 + symbolTable.GetIndex(param)) * 2);
                append("Load_Word " + pushRegister() + ", a");

                append("Mov a, BP");
                append("Sub_Immediate a, {0}", (2 + symbolTable.GetIndex(param) * 2));
                append("Store_Word a, {0}", popRegister());
            }

            node.VisitChildren(this);
            symbolTable.PopScope();
            append("Add_Immediate SP, {0}", symbolTable.GetIndex(node.Name) * 2);
            append("Pop BP");
            append("Ret");
        }
        public void Accept(ConditionalNode node)
        {
            node.Predicate.Visit(this);
            string elseSymbol = "symbol" + nextSymbol++;
            string endSymbol = "symbol" + nextSymbol++;
            append("And {0}, {1}", getRegister(), getRegister());
            append("Jne {0}", elseSymbol);
            node.Body.Visit(this);
            append("Jmp {0}", endSymbol);
            append(".{0}", elseSymbol);
            node.ElseBody.Visit(this);
            append(".{0}", endSymbol);

        }
        public void Accept(ExpressionNode node) {}
        public void Accept(FunctionCallNode node)
        {
            node.Arguments.Visit(this);
            string call = ((IdentifierNode)node.Target).Identifier;
            append("Call {0}", call);
            append("Add_Immediate SP, {0}", node.Arguments.Children.Count * 2);
        }
        public void Accept(IdentifierNode node)
        {
            append("Mov a, BP");
            append("Sub_Immediate a, {0}", (2 + symbolTable.GetIndex(node.Identifier) * 2));
            append("Load_Word {0}, a", pushRegister());
        }
        public void Accept(NumberNode node)
        {
            append("Load_Immediate {0}, {1}", pushRegister(), node.Number);
        }
        public void Accept(StatementNode node) {}
        public void Accept(WhileNode node) {}
        public void Accept(UnaryOperationNode node)
        {
            switch (node.UnaryOperation)
            {
                case UnaryOperation.Not:
                    node.Body.VisitChildren(this);
                    append("Not {0}", getRegister());
                    break;
                case UnaryOperation.Reference:
                    pushRegister();
                    append("Mov {0}, BP", getRegister());
                    append("Sub_Immediate {0}, {1}", getRegister(), (2 + symbolTable.GetIndex(((IdentifierNode)node.Body).Identifier) * 2));
                    break;
            }
        }

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

        private void append(string line, params object[] args)
        {
            result.AppendLine(string.Format(line, args));
        }
    }
}