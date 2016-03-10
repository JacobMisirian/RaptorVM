using System;
using System.IO;
using System.Text;

namespace RaptorDisassembler
{
    public class Disassembler
    {
        private BinaryReader reader;
        public Disassembler(string path)
        {
            reader = new BinaryReader(new StreamReader(path).BaseStream);
        }

        public string Disassemble()
        {
            StringBuilder ret = new StringBuilder();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string op = OpCodes.ToString(reader.ReadByte());
                ret.Append(op);

                switch (op)
                {
                    case "Add":
                    case "Sub":
                    case "Mul":
                    case "Div":
                    case "Mod":
                    case "Mov":
                        ret.Append("\t");
                        ret.Append(getRegister(reader.ReadByte()));
                        ret.Append(", ");
                        ret.Append(getRegister(reader.ReadByte()));
                        reader.ReadInt16();
                        break;
                    case "Load_Immediate":
                        ret.Append("\t");
                        ret.Append(getRegister(reader.ReadByte()));
                        ret.Append(", ");
                        reader.ReadByte();
                        ret.Append(reader.ReadInt16());
                        break;
                    case "Print":
                        ret.Append("\t");
                        ret.Append(getRegister(reader.ReadByte()));
                        reader.ReadByte();
                        reader.ReadInt16();
                        break;
                }
                ret.Append("\n");
            }
            return ret.ToString();
        }

        private char getRegister(byte b)
        {
            return (char)(b + 96);
        }
    }
}

