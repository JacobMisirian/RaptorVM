using System;
using System.IO;

namespace RaptorDisassembler
{
    public class Instruction
    {
        public byte OpCode { get; private set; }
        public string OperandOne { get { return ((char)Convert.ToInt32(operandOne + 96)).ToString(); } }
        public string OperandTwo { get { return ((char)Convert.ToInt32(operandTwo + 96)).ToString(); } }
        public Int16 Immediate { get; private set; }
        private byte operandOne;
        private byte operandTwo;
        public Instruction(BinaryReader reader)
        {
            OpCode = reader.ReadByte();
            byte operands = reader.ReadByte();
            operandOne = operands;
            operandTwo = operands;
            Immediate = reader.ReadInt16();
        }
    }
}

