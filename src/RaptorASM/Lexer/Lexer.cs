using System;
using System.Collections.Generic;

namespace RaptorASM
{
    public class Lexer
    {
        private string code;
        private int position = 0;
        private List<Token> result = new List<Token>();

        public Lexer(string source)
        {
            code = source;
        }

        public List<Token> Scan()
        {
            whiteSpace();
            while (peekChar() != -1)
            {
                if (char.IsLetterOrDigit((char)peekChar()))
                    result.Add(scanData());
                else
                {
                    switch ((char)peekChar())
                    {
                        case ',':
                            result.Add(new Token(TokenType.Comma, ((char)readChar()).ToString()));
                            break;
                        default:
                            Console.WriteLine("Unknown char: " + (char)readChar());
                            break;
                    }
                }
                whiteSpace();
            }

            return result;
        }

        private void whiteSpace()
        {
            while (char.IsWhiteSpace((char)peekChar()))
                position++;
        }

        private Token scanData()
        {
            string result = "";
            double temp = 0;
            while ((char.IsLetterOrDigit((char)peekChar()) && peekChar() != -1) || ((char)(peekChar()) == '.') || ((char)peekChar()) == '_')
                result += ((char)readChar()).ToString();
            if (double.TryParse(result, out temp))
                return new Token(TokenType.Number, result);

            return new Token(TokenType.Identifier, result);
        }

        private int peekChar(int n = 0)
        {
            return position + n < code.Length ? code[position] : -1;
        }

        private int readChar()
        {
            return position < code.Length ? code[position++] : -1;
        }
    }
}

