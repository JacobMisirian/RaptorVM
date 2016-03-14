using System;

namespace RaptorB.Lexer
{
    public class Token
    {
        public TokenType TokenType { get; private set; }
        public object Value { get; private set; }

        public Token(TokenType tokenType, object value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public override string ToString()
        {
            return TokenType + "\t" + Value;
        }
    }

    public enum TokenType
    {
        Assignment,
        Identifier,
        Number,
        Char,
        Dot,
        Comma,
        Parentheses,
        Bracket,
        Operation,
        Comparison,
        Semicolon
    }
}

