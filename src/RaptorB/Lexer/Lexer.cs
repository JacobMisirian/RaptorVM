using System;
using System.Collections.Generic;

namespace RaptorB.Lexer
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
                        case '\'':
                            result.Add(scanChar());
                            break;
                        case '+':
                        case '-':
                        case '*':
                        case '/':
                        case '%':
                            result.Add(new Token(TokenType.Operation, ((char)readChar()).ToString()));
                            break;
                        case '>':
                        case '<':
                            string op = ((char)readChar()).ToString();
                            if ((char)peekChar() == '=')
                                op += ((char)readChar()).ToString();
                            result.Add(new Token(TokenType.Comparison, op));
                            break;
                        case '=':
                            position++;
                            if ((char)peekChar() == '=')
                            {
                                result.Add(new Token(TokenType.Comparison, "=="));
                                position++;
                            }
                            else
                                result.Add(new Token(TokenType.Assignment, "="));
                            break;
                        case ',':
                            result.Add(new Token(TokenType.Comma, ((char)readChar()).ToString()));
                            break;
                        case '(':
                        case ')':
                            result.Add(new Token(TokenType.Parentheses, ((char)readChar()).ToString()));
                            break;
                        case '{':
                        case '}':
                            result.Add(new Token(TokenType.Bracket, ((char)readChar()).ToString()));
                            break;
                        case ';':
                            result.Add(new Token(TokenType.Semicolon, ((char)readChar()).ToString()));
                            break;
                        default:
                            Console.WriteLine("Unknown char: " + ((char)readChar()).ToString());
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

        private Token scanChar()
        {
            // '
            position++;
            char result = (char)readChar();
            // '
            position++;
            return new Token(TokenType.Char, result);
        }

        private Token scanData()
        {
            string result = "";
            while ((char.IsLetterOrDigit((char)peekChar()) || (char)peekChar() == '_') && peekChar() != -1)
                result += ((char)readChar()).ToString();
            try
            {
                return new Token(TokenType.Number, Convert.ToInt16(result));
            }
            catch
            {
                return new Token(TokenType.Identifier, result);
            }
        }

        private int peekChar(int n = 0)
        {
            return (position + n) < code.Length ? code[position] : -1;
        }

        private int readChar()
        {
            return position < code.Length ? code[position++] : -1;
        }
    }
}

