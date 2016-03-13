using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RaptorASM
{
    public class Assembler
    {
        private List<Token> tokens;
        private Dictionary<string, long> labels = new Dictionary<string, long>();
        private List<LabelReference> references = new List<LabelReference>();
        private BinaryWriter writer;
        private int position = 0;

        public Assembler(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void Assemble(string outputPath = "a.out")
        {
            writer = new BinaryWriter(new StreamWriter(outputPath).BaseStream);

            for (position = 0; position < tokens.Count;)
            {
                if (tokens[position].TokenType == TokenType.Dot)
                {
                    position++;
                    labels.Add(expectToken(TokenType.Identifier).Value, writer.BaseStream.Position);
                    continue;
                }

                Token op = expectToken(TokenType.Identifier);

                if (op.Value == "STRING")
                {
                    writer.Write(Encoding.ASCII.GetBytes(expectToken(TokenType.String).Value));
                    writer.Write((byte)0);
                    writer.Flush();
                    continue;
                }

                byte opcode = OpCodes.ToByte(op.Value);
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
                    case OpCodes.Shift_Left:
                    case OpCodes.Shift_Right:
                    case OpCodes.And:
                    case OpCodes.Or:
                    case OpCodes.Xor:
                    case OpCodes.Cmp:
                    case OpCodes.Load_Byte:
                    case OpCodes.Load_Word:
                    case OpCodes.Store_Word:
                    case OpCodes.Store_Byte:
                        registerOne = getRegister(expectToken(TokenType.Identifier).Value);
                        expectToken(TokenType.Comma);
                        registerTwo = getRegister(expectToken(TokenType.Identifier).Value);
                        new Instruction(opcode, registerOne, registerTwo).Encode(writer);
                        break;
                    case OpCodes.Add_Immediate:
                    case OpCodes.Sub_Immediate:
                    case OpCodes.Mul_Immediate:
                    case OpCodes.Div_Immediate:
                    case OpCodes.Mod_Immediate:
                    case OpCodes.Mov_Immediate:
                    case OpCodes.Shift_Left_Immediate:
                    case OpCodes.Shift_Right_Immediate:
                    case OpCodes.And_Immediate:
                    case OpCodes.Or_Immediate:
                    case OpCodes.Xor_Immediate:
                    case OpCodes.Cmp_Immediate:
                        registerOne = getRegister(expectToken(TokenType.Identifier).Value);
                        expectToken(TokenType.Comma);
                        immediate = Convert.ToInt16(expectToken(TokenType.Number).Value);
                        new Instruction(opcode, registerOne, 0, immediate).Encode(writer);
                        break;
                    case OpCodes.Not:
                        registerOne = getRegister(expectToken(TokenType.Identifier).Value);
                        new Instruction(opcode, registerOne).Encode(writer);
                        break;
                    case OpCodes.Load_Immediate:
                        registerOne = getRegister(expectToken(TokenType.Identifier).Value);
                        expectToken(TokenType.Comma);
                        if (tokens[position].TokenType == TokenType.Identifier)
                        {
                            references.Add(new LabelReference(expectToken(TokenType.Identifier).Value, writer.BaseStream.Position));
                            immediate = 0;
                        }
                        else
                            immediate = Convert.ToInt16(expectToken(TokenType.Number).Value);
                        new Instruction(opcode, registerOne, 0, immediate).Encode(writer);
                        break;
                    case OpCodes.Print:
                    case OpCodes.Print_Char:
                    case OpCodes.Inc:
                    case OpCodes.Dec:
                    case OpCodes.Push:
                    case OpCodes.Pop:
                        new Instruction(opcode, getRegister(expectToken(TokenType.Identifier).Value)).Encode(writer);
                        break;
                    case OpCodes.Print_Immediate:
                    case OpCodes.Print_Char_Immediate:
                        new Instruction(opcode, 0, 0, Convert.ToInt16(expectToken(TokenType.Number).Value)).Encode(writer);
                        break;
                    case OpCodes.Jmp:
                    case OpCodes.Je:
                    case OpCodes.Jne:
                    case OpCodes.Jl:
                    case OpCodes.Jle:
                    case OpCodes.Jg:
                    case OpCodes.Jge:
                    case OpCodes.Call:
                        references.Add(new LabelReference(expectToken(TokenType.Identifier).Value, writer.BaseStream.Position));
                        new Instruction(opcode).Encode(writer);
                        break;
                    case OpCodes.Ret:
                        new Instruction(opcode).Encode(writer);
                        break;
                }
            }

            foreach (LabelReference reference in references)
            {
                writer.BaseStream.Position = reference.Position + 2;
                writer.Write((short)labels[reference.Name]);
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

