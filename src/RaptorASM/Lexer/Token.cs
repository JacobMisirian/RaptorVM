using System;

namespace RaptorASM
{
    public class Token
    {
        public TokenType TokenType { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }
    }

    public enum TokenType
    {
        Comma,
        Identifier,
        Number
    }
}

