using System;
using System.Collections.Generic;
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
        private Dictionary<string, string> strings;

        public CodeGenerator(AstNode ast, SymbolTable symbolTable)
        {
            this.ast = ast;
            this.symbolTable = symbolTable;
        }

        public string Generate()
        {
            result = new StringBuilder();
            strings = new Dictionary<string, string>();
            append("Load_Immediate SP, 6000");
            append("Mov BP, SP");
            append("Call main");
            append(".hang Jmp hang");
            ast.VisitChildren(this);
            append(putchar);
            append(putint);
            append(putstr);
            append(charat);
            writeStrings();
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
                    append("Sub_Immediate a, {0}",  (2 + symbolTable.GetIndex(((IdentifierNode)node.Left).Identifier) * 2));
                    append("Store_Word a, {0}", popRegister());
                    //pushRegister();
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
                append("Sub_Immediate a, {0}",  (2 + symbolTable.GetIndex(param) * 2));
                append("Store_Word a, {0}", popRegister());
            }

            node.VisitChildren(this);
            symbolTable.PopScope();
            append("Add_Immediate SP, {0}", symbolTable.GetGlobalIndex(node.Name) * 2);
            append("Pop BP");
            append("Ret");
        }
        public void Accept(ConditionalNode node)
        {
            node.Predicate.Visit(this);
            string elseSymbol = generateSymbol();
            string endSymbol = generateSymbol();
            string register = getRegister();
            append("And {0}, {1}", register, register);
            append("Jne {0}", elseSymbol);
            node.Body.Visit(this);
            append("Jmp {0}", endSymbol);
            append(".{0}", elseSymbol);
            if (node.Children.Count > 2)
                node.ElseBody.Visit(this);
            append(".{0}", endSymbol);
            popRegister();

        }
        public void Accept(ExpressionNode node) {}
        public void Accept(FunctionCallNode node)
        {
            node.Arguments.Visit(this);
            string call = ((IdentifierNode)node.Target).Identifier;
            append("Call {0}", call);
            append("Add_Immediate SP, {0}", node.Arguments.Children.Count * 2);
            append("Mov {0}, a", getRegister());
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
        public void Accept(StringNode node)
        {
            string symbol = generateStringSymbol();
            append("Load_Immediate {0}, {1}", pushRegister(), symbol);
            strings.Add(symbol, node.String);
        }
        public void Accept(WhileNode node)
        {
            string whileSymbol = generateSymbol();
            string endSymbol = generateSymbol();
            node.Predicate.Visit(this);
            string register = getRegister();
            append("And {0}, {1}", register, register);
            popRegister();
            append("Jne {0}", endSymbol);
            append(".{0}", whileSymbol);
            node.Body.Visit(this);
            node.Predicate.Visit(this);
            popRegister();
            append("Je {0}", whileSymbol);
            append(".{0}", endSymbol);
        }
        public void Accept(UnaryOperationNode node)
        {
            string source, dest;
            switch (node.UnaryOperation)
            {
                case UnaryOperation.Not:
                    node.Body.VisitChildren(this);
                    append("Not {0}", pushRegister());
                    break;
                case UnaryOperation.Reference:
                    dest = pushRegister();
                    append("Mov a, BP");
                    append("Sub_Immediate a, {0}", (2 + symbolTable.GetIndex(((IdentifierNode)node.Body).Identifier) * 2));
                    append("Mov {0}, a", dest);
                    break;
                case UnaryOperation.Dereference:
                    node.Body.Visit(this);
                    dest = popRegister();
                    source = pushRegister();
                    append("Load_Word {0}, {1}", dest, source);
                    break;
            }
        }

        private const string putchar = ".putchar Push BP Mov BP, SP Mov a, BP Add_Immediate a, 4 Load_Word b, a Print_Char b Pop BP Ret";
        private const string putint = ".putint Push BP Mov BP, SP Mov a, BP Add_Immediate a, 4 Load_Word b, a Print b Pop BP Ret";
        private const string putstr = ".putstr Push BP Mov BP, SP Mov a, BP Add_Immediate a, 4 Load_Word b, a Call writeStr Pop BP Ret .writeStr Load_Byte a, b Print_Char a Cmp_Immediate a, 0 Inc b Jne writeStr Ret";
        private const string charat = ".charat Push BP Mov BP, SP Mov a, BP Add_Immediate a, 4 Load_Word b, a Mov a, BP Add_Immediate a, 6 Load_Word a, a Add a, b Load_Word a, a Pop BP Ret";

        private int currentRegister = (int)'b';

        private int positionInRam(string identifier)
        {
            return (2 * symbolTable.GetIndex(identifier) + 2);
        }

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

        private int nextSymbol = 0;
        private string generateSymbol()
        {
            return "symbol" + nextSymbol++;
        }
        private int nextStringSymbol = 0;
        private string generateStringSymbol()
        {
            return "symbolString" + nextStringSymbol++;
        }

        private void writeStrings()
        {
            foreach (KeyValuePair<string, string> entry in strings)
            {
                append(".{0}", entry.Key);
                append("STRING \"{0}\"", entry.Value);
            }
        }

        private void append(string line, params object[] args)
        {
            result.AppendLine(string.Format(line, args));
        }
    }
}