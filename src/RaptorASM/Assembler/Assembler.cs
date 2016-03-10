using System;
using System.Collections.Generic;
using System.IO;

namespace RaptorASM
{
    public class Assembler
    {
        private List<Token> tokens;
        private BinaryWriter writer;
        private int position = 0;

        public Assembler(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void Assemble(string outputPath = "a.out")
        {
            writer = new BinaryWriter(new StreamWriter(outputPath).BaseStream);

            for (position = 0; position < tokens.Count;     )
            {
                byte opcode = OpCodes.ToByte(expectToken(TokenType.Identifier).Value);
                byte registerOne;
                byte registerTwo;
                short immediate;
                switch (opcode)
                {
                    case OpCodes.Add:
                    case OpCodes.Sub:
                    case OpCodes.Mul:
                    case OpCodes.Div:
                    case OpCodes.Mod:
                    case OpCodes.Mov:
                        registerOne = getRegister(expectToken(TokenType.Identifier).Value);
                        expectToken(TokenType.Comma);
                        registerTwo = getRegister(expectToken(TokenType.Identifier).Value);
                        new Instruction(opcode, registerOne, registerTwo).Encode(writer);
                        break;
                    case OpCodes.Load_Immediate:
                        registerOne = getRegister(expectToken(TokenType.Identifier).Value);
                        expectToken(TokenType.Comma);
                        immediate = Convert.ToInt16(expectToken(TokenType.Number).Value);
                        new Instruction(opcode, registerOne, 0, immediate).Encode(writer);
                        break;
                    case OpCodes.Print:
                        new Instruction(opcode, getRegister(expectToken(TokenType.Identifier).Value)).Encode(writer);
                        break;
                }
            }
        }

        private Token expectToken(TokenType tokenType)
        {
            if (tokens[position].TokenType != tokenType)
                throw new Exception("Expected " + tokenType + " got " + tokens[position].TokenType);
            return tokens[position++];
        }

        private byte getRegister(string registerString)
        {
            int c = Convert.ToChar(registerString);
            if (c < 97 || c > 112)
                throw new Exception("Unknown register " + registerString);
            return (byte)(c - 96);
        }
    }
}

