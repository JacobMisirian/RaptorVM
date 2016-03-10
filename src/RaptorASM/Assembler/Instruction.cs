using System;
using System.IO;

namespace RaptorASM
{
    public class Instruction
    {
        public byte OpCode { get; private set; }
        public byte OperandOne { get; private set; }
        public byte OperandTwo { get; private set; }
        public short Immediate { get; private set; }

        public Instruction(byte opcode, byte operandOne = 0, byte operandTwo = 0, short immediate = 0)
        {
            OpCode = opcode;
            OperandOne = operandOne;
            OperandTwo = operandTwo;
            Immediate = immediate;
        }

        public void Encode(BinaryWriter writer)
        {
            writer.Write(OpCode);
            writer.Write(OperandOne);
            writer.Write(OperandTwo);
            writer.Write(Immediate);

            writer.Flush();
        }
    }
}

