using System;
using System.IO;
using System.Text;

namespace RaptorDisassembler
{
    public class Disassembler
    {
        private BinaryReader reader;
        private StringBuilder ret = new StringBuilder();

        public Disassembler(string path)
        {
            reader = new BinaryReader(new StreamReader(path).BaseStream);
        }

        public string Disassemble()
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Instruction instruction = new Instruction(reader);
                switch (instruction.OpCode)
                {
                    case OpCodes.Add:
                    case OpCodes.Sub:
                    case OpCodes.Mul:
                    case OpCodes.Div:
                    case OpCodes.Mod:
                        append("{0}\t{1}, {2}", OpCodes.ToString(instruction.OpCode), instruction.OperandOne, instruction.OperandTwo);
                        break;
                }
            }
            return ret.ToString();
        }

        private void append(string str, params object[] args)
        {
            ret.AppendLine(string.Format(str, args));
        }
    }
}