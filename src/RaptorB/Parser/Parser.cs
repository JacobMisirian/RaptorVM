using System;
using System.Collections.Generic;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class Parser
    {
        public bool EndOfStream { get { return tokens.Count >= position; } }
        public int Position { get { return position; } set { position = value; } }
        private int position = 0;

        private List<Token> tokens;
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public AstNode Parse()
        {
            CodeBlockNode tree = new CodeBlockNode();
            while (position < tokens.Count)
                tree.Children.Add(StatementNode.Parse(this));
            return tree;
        }

        public bool MatchToken(TokenType clazz)
        {
            return Position < tokens.Count && tokens[Position].TokenType == clazz;
        }

        public bool MatchToken(TokenType clazz, string value)
        {
            return Position < tokens.Count && tokens[Position].TokenType == clazz && ((string)tokens[Position].Value).ToUpper() == value.ToUpper();
        }

        public bool AcceptToken(TokenType clazz)
        {
            if (MatchToken(clazz))
            {
                Position++;
                return true;
            }

            return false;
        }

        public bool AcceptToken(TokenType clazz, string value)
        {
            if (MatchToken(clazz, value.ToUpper()))
            {
                Position++;
                return true;
            }

            return false;
        }

        public Token ExpectToken(TokenType clazz)
        {
            if (!MatchToken(clazz))
                throw new Exception("Tokens did not match. Expected " + clazz);

            return tokens[Position++];
        }

        public Token ExpectToken(TokenType clazz, string value)
        {
            if (!MatchToken(clazz, value.ToUpper()))
                throw new Exception("Tokens did not match. Expected " + clazz + " of value " + value);

            return tokens[Position++];
        }

        public Token ReadToken(int n = 0)
        {
            return tokens[Position + n];
        }

        public Token CurrentToken(int n = 0)
        {
            if (tokens.Count > Position + n)
                return tokens[Position + n];

            return new Token(TokenType.Identifier, "");
        }
    }
}

